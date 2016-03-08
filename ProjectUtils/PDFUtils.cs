using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace ProjectUtils
{
    public static class PDFUtils
    {
        public static MemoryStream GeneratePDFForm(string form_name, Dictionary<string, string> replacements)
        {
            if (HttpContext.Current == null)
            {
                throw new InvalidOperationException("GeneratePDFForm called without a HttpContext");
            }

            string template_path = HttpContext.Current.Server.MapPath(String.Format("~/templates/{0}.pdf", form_name));

            PdfReader pdfTemplate = new PdfReader(template_path);
            Document doc = new Document();
            MemoryStream msOutput = new MemoryStream();
            PdfCopy pCopy = new PdfCopy(doc, msOutput);

            doc.Open();

            MemoryStream msTemp = new MemoryStream();

            PdfStamper stamper = new PdfStamper(pdfTemplate, msTemp);

            foreach(KeyValuePair<string, string> kvp in replacements)
            {
                stamper.AcroFields.SetField(kvp.Key, kvp.Value);
            }

            stamper.FormFlattening = true;
            stamper.Close();

            PdfReader tempPDF = new PdfReader(msTemp.ToArray());

            pCopy.AddPage(pCopy.GetImportedPage(tempPDF, pdfTemplate.NumberOfPages));
            pCopy.FreeReader(tempPDF);

            doc.Close();

            return msOutput;
        }

    }
}
