﻿using Aspose.Words.Drawing;
using Aspose.Words;
using Dtos.AlphaBank.AnalyzeLoanOpportunities;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout.Element;

namespace Service.AnalyzeLoanOpportunities.Helper {
    internal static class FileConverterHelper {

        public static byte[] ConvertTxtToPdf(FileUploadDTO oFileUploadDTO, string pdfTitle) {

            using MemoryStream ms = new();

            using (PdfWriter writer = new(ms)) {
                using PdfDocument pdf = new(writer);

                // Set PDF title
                pdf.GetDocumentInfo().SetTitle(pdfTitle);

                iText.Layout.Document doc = new(pdf);

                //Write the content of the file to the PDF
                string textContent = System.Text.Encoding.UTF8.GetString(oFileUploadDTO.FileContent);
                iText.Layout.Element.Paragraph paragraph = new(textContent);
                doc.Add(paragraph);

                doc.Close();
            }

            return ms.ToArray();
        }

        public static byte[] ConvertWordToPdf(FileUploadDTO oFileUploadDTO, string pdfTitle) {
            // Load the Word document from the byte array
            using MemoryStream input = new(oFileUploadDTO.FileContent);
            // Create a new MemoryStream to save the PDF
            using MemoryStream output = new();

            // Initialize PDF writer
            PdfWriter writer = new(output);

            // Initialize PDF document
            PdfDocument pdf = new(writer);

            // Initialize document
            iText.Layout.Document doc = new(pdf);

            // Load the Word document
            Aspose.Words.Document wordDoc = new(input);

            // Iterate through sections of the Word document
            foreach (Aspose.Words.Section wordSection in wordDoc.Cast<Aspose.Words.Section>()) {
                // Iterate through paragraphs of the section
                foreach (Aspose.Words.Paragraph wordParagraph in wordSection.Body.Paragraphs.Cast<Aspose.Words.Paragraph>()) {
                    // Convert each paragraph to a PDF paragraph and add it to the PDF document
                    iText.Layout.Element.Paragraph pdfParagraph = new(wordParagraph.GetText());
                    doc.Add(pdfParagraph);
                }
            }

            // Process inline images
            NodeCollection shapes = wordDoc.GetChildNodes(NodeType.Shape, true);
            foreach (Shape shape in shapes.Cast<Shape>()) {
                if (shape.HasImage) {
                    // Extract image data and add it to PDF
                    MemoryStream imageData = new();
                    shape.ImageData.Save(imageData);
                    // Add image data to PDF document
                    Image image = new (ImageDataFactory.Create(imageData.ToArray()));
                    doc.Add(image);
                }
            }

            // Set PDF title
            pdf.GetDocumentInfo().SetTitle(pdfTitle);

            // Close the document
            doc.Close();

            // Return the converted PDF as byte array
            return output.ToArray();
        }

        public static byte[] CopyPdf(FileUploadDTO oFileUploadDTO, string pdfTitle) {
            // Create a MemoryStream to load the original PDF
            using MemoryStream input = new(oFileUploadDTO.FileContent);

            // Create a MemoryStream to store the copied PDF
            using MemoryStream output = new();

            // Initialize the PDF writer
            PdfWriter writer = new(output);

            // Initialize the PDF reader
            PdfReader reader = new(input);

            // Initialize the PDF document
            PdfDocument pdf = new(reader, writer);

            // Set the title of the PDF
            pdf.GetDocumentInfo().SetTitle(pdfTitle);

            // Close the PDF document (no changes needed, just copying the PDF)
            pdf.Close();

            // Return the copied PDF as a byte array
            return output.ToArray();
        }
    }
}