using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ELF
{
    public partial class NotesForm : Form
    {
        public string Title { get { return this.Text; } set { this.Text = value; } }
        public string Message { get { return NotesText.Text; } set { NotesText.Text = value; } }

        public NotesForm()
        {
            InitializeComponent();
        }
    }
}
