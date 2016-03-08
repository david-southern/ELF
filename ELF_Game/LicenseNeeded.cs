using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using IntelliLock.Licensing;
using ELF_Resources.Properties;
using System.IO;
using System.Net;

namespace ELF
{
    public partial class LicenseNeeded : Form
    {
        public LicenseNeeded()
        {
            InitializeComponent();
        }

        private void LicenseNeeded_Load(object sender, EventArgs e)
        {
        }

        public ELFServices.ELFServices GetServicesBinding()
        {
            ELFServices.ELFServices services = new ELF.ELFServices.ELFServices();
            string hostname = Dns.GetHostName();

            services.Url = "https://www.e-l-fun.com/ELFServices.asmx";

            string servicesUrlOverride = Environment.GetEnvironmentVariable("ELF_SERVICES_URL");

            if (servicesUrlOverride != null)
            {
                if (servicesUrlOverride.StartsWith("http"))
                {
                    services.Url = servicesUrlOverride;
                }
                else
                {
                    services.Url = "http://localhost/ELFServices.asmx";
                }
            }

            return services;
        }


        private void Unlock_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(ActivationCode.Text.Trim()))
            {
                MessageBox.Show("You did not enter an activation code");
                return;
            }

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

            ELFServices.ELFServices services = GetServicesBinding();

            string hardwareID = HardwareID.GetHardwareID(true, true, true, true, true, false);

            Dictionary<string, string> clientInfo = new Dictionary<string, string>();
            clientInfo[ProjectUtils.Constants.CLIENT_INFO_CLIENT_VERSION] = ProjectUtils.Utils.GetProgramVersionInt().ToString();
            clientInfo[ProjectUtils.Constants.CLIENT_INFO_MONITOR_SIZE] = SystemInformation.PrimaryMonitorSize.ToString();
            clientInfo[ProjectUtils.Constants.CLIENT_INFO_OS_VERSION] = Environment.OSVersion.VersionString.ToString();
            clientInfo[ProjectUtils.Constants.CLIENT_INFO_CLR_VERSION] = Environment.Version.ToString();

            string registrationResult = services.FinishRegistrationRev001(ActivationCode.Text,
                hardwareID, ProjectUtils.Utils.MarshallClientInfo(clientInfo));

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;

            if (!registrationResult.StartsWith("Success:"))
            {
                NotesDialog notes = new NotesDialog();
                notes.Text = "Registration Results";
                notes.NotesText.Text = registrationResult;
                notes.ShowDialog();
                return;
            }

            byte[] license = Convert.FromBase64String(registrationResult.Substring(8));

            string license_filename = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\ELF";

            if (!Directory.Exists(license_filename))
            {
                Directory.CreateDirectory(license_filename);
            }

            license_filename += "\\license.elfcode";

            using (FileStream writer = new FileStream(license_filename, FileMode.Create))
            {
                writer.Write(license, 0, license.Length);
                writer.Close();
            }

            if (ProjectUtils.Utils.IsValidLicense())
            {
                MessageBox.Show("Thank you for your purchase.  ELF is now fully unlocked.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                NotesForm err = new NotesForm();

                err.Message = String.Format("There was a problem activating your license.  This can be caused if you "
                    + "are using an Activation Code that was generated for a different computer, or if you have "
                    + "upgraded your computer's hardware since the Activation Code was created.  "
                    + "Please contact {3} with this information:"
                    + "{0}" 
                    + "{0}Activation Code: {1}"
                    + "{0}Hardware ID: {2}"
                    + "{0}{0}Please also include the email address that you initially used to register ELF",
                    Environment.NewLine,
                    ActivationCode.Text, hardwareID, ProjectUtils.Constants.SUPPORT_EMAIL);

                err.ShowDialog();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = (string)linkLabel1.Text;
            System.Diagnostics.Process.Start(url);
        }
    }
}
