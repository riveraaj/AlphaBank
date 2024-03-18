using Dtos.AlphaBank.AnalyzeLoanOpportunities;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace Service.AnalyzeLoanOpportunities.Helper {
    public static class FileConverterHelper {

        public static byte[] ConvertTxtToPdf(FileUploadDto oFileUploadDto, string pdfTitle){

            using MemoryStream ms = new();

            using (PdfWriter writer = new(ms)) {
                using PdfDocument pdf = new(writer);

                // Set PDF title
                pdf.GetDocumentInfo().SetTitle(pdfTitle);

                Document doc = new(pdf);

                //Write the content of the file to the PDF
                string textContent = System.Text.Encoding.UTF8.GetString(oFileUploadDto.FileContent);
                Paragraph paragraph = new(textContent);
                doc.Add(paragraph);

                doc.Close();
            }

            return ms.ToArray();
        }

        public static byte[] ConvertWordToPdf(FileUploadDto oFileUploadDto, string pdfTitle) {

            // Load the Word document from the byte array
            using MemoryStream input = new (oFileUploadDto.FileContent);
            // Create a new MemoryStream to save the PDF
            using MemoryStream output = new ();

            // Initialize PDF writer
            PdfWriter writer = new(output);

            // Initialize PDF document
            PdfDocument pdf = new(writer);

            // Initialize document
            Document doc = new(pdf);

            // Load the Word document
            Aspose.Words.Document wordDoc = new(input);

            // Iterate through sections of the Word document
            foreach (Aspose.Words.Section wordSection in wordDoc.Cast<Aspose.Words.Section>()) {
                // Iterate through paragraphs of the section
                foreach (Aspose.Words.Paragraph wordParagraph in wordSection.Body.Paragraphs.Cast<Aspose.Words.Paragraph>()) {
                    // Convert each paragraph to a PDF paragraph and add it to the PDF document
                    Paragraph pdfParagraph = new(wordParagraph.GetText());
                    doc.Add(pdfParagraph);
                }
            }

            // Set PDF title
            pdf.GetDocumentInfo().SetTitle(pdfTitle);

            // Close the document
            doc.Close();

            // Return the converted PDF as byte array
            return output.ToArray();
        }
    }
}