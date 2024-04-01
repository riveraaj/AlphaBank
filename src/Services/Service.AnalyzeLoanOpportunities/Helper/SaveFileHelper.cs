namespace Service.AnalyzeLoanOpportunities.Helper {
    internal static class SaveFileHelper {

        private const string filePath = @"C:\Documents\Proyecto-SoftwareIII\Solicitud-Prestamo";

        public static string SaveFile(byte[] pdfContent, string pdfName) {
            // If the folders doesn´t exists, then they will be created
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            //Merge the file name with the directory path to get the full path
            string fullFilePath = Path.Combine(filePath, pdfName + ".pdf");

            //Write the content of the PDF to the specified file
            File.WriteAllBytes(fullFilePath, pdfContent);

            return fullFilePath;
        }
    }
}