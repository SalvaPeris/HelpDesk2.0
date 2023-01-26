using System;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace HelpDesk.Client.Utils.PDF
{
    public static class PDF
    {
        public static byte[] CrearPDF(string greeting)
        {
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            PdfWriter pdfWriter = new PdfWriter(memoryStream);
            PdfDocument pdfDocument = new PdfDocument(pdfWriter);
            Document document = new Document(pdfDocument);
            Paragraph paragraph = new Paragraph(greeting)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(20);
            document.Add(paragraph);
            document.Close();
            return memoryStream.ToArray();
        }


        public static byte[] CrearPDFDesdeFileStream(MemoryStream memoryStream)
        {
            PdfWriter pdfWriter = new PdfWriter(memoryStream);
            PdfDocument pdfDocument = new PdfDocument(pdfWriter);
            Document document = new Document(pdfDocument);
            document.Close();
            return memoryStream.ToArray();
        }

        public static byte[] PDFDesdeFileStream(MemoryStream memoryStream)
        {
            return memoryStream.ToArray();
        }
    }
}

