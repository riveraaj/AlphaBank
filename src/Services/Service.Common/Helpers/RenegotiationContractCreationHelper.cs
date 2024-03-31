using Data.AlphaBank;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout;
using System.Globalization;
using Humanizer;

namespace Service.Common.Helpers
{
    public static class RenegotiationContractCreationHelper
    {
        private const string filePath = @"C:\Documents\Proyecto-SoftwareIII\Renegociaciones";

        public static string CreatePdf(Loan oLoan, string newInterestPercentage, string newLoanTerm, decimal newLoanAmount)
        {
            // If the folders doesn´t exists, then they will be created
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            //Get the CultureInfo from Costa Rica because this will be used by Humanizer Library
            var spanish = new CultureInfo("es-CR");

            //Get the currency description of the LoanApplication
            string currency = oLoan.LoanApplication.TypeCurrency.Description;
            //Get the todays date in string format
            string date = DateTime.Now.ToString("dd 'de' MMMM 'del' yyyy", spanish);
            //Get the Name, FirstName and SecondName of the Customer Owner of the Account and LoanApplication, then concatenate them into the full name
            string clientFullName = oLoan.LoanApplication.Account.Customer.Person.Name + " "
                + oLoan.LoanApplication.Account.Customer.Person.FirstName + " "
                + oLoan.LoanApplication.Account.Customer.Person.SecondName;

            //Get the TypeIdentification Description of the Customer(Person)
            string identificationType = oLoan.LoanApplication.Account.Customer.Person.TypeIdentification.Description;

            //The IdentificationNumber will be StringFormated based on the TypeIdentificationId
            string identificationNumber = oLoan.LoanApplication.Account.Customer.Person.TypeIdentificationId switch
            {
                1 => oLoan.LoanApplication.Account.Customer.PersonId.ToString("#-####-####"),// TypeIdentificationId 1 is for "Física" so the format will be assing to the string
                2 => oLoan.LoanApplication.Account.Customer.PersonId.ToString("#-###-######"),// TypeIdentificationId 2 is for "Jurídica" so the format will be assing to the string
                _ => oLoan.LoanApplication.Account.Customer.PersonId.ToString(),// Default format
            };

            //Get the Identification Number of the Loan
            int loandID = oLoan.Id;
            //Get the address of the Customer(Person)
            string address = oLoan.LoanApplication.Account.Customer.Person.Address;
            //Get the loan requested amount of the LoanApplication
            decimal loanAmount = oLoan.LoanApplication.Amount;
            // Get the loanTerm, taking on consideration that the DeadLine description is in  format "# meses" 
            // the description will be splited by " ". Then we only get the first element of the list, so it will be "#". 
            string newloanTerm = newLoanTerm.Split(' ')[0];
            // Get the PaymentAmount of the Loan dividing the LoanAmount by the LoanTerm (this amount doesn´t include interests)
            decimal newPaymentAmount = (newLoanAmount / decimal.Parse(newloanTerm));
            // Get the description or reason of the LoanApplication
            string description = oLoan.LoanApplication.Justification;


            // Combine the filePath(Where the contracts will be stored) with the pdf File Name (LoanApplicationId_loan_contract_PersonId.pdf)
            string finalFilePath = System.IO.Path.Combine(filePath, oLoan.LoanApplication.Id + "_loan_renegotiation_"
                + oLoan.LoanApplication.Account.Customer.PersonId + ".pdf");

            try
            {
                PdfWriter writer = new(finalFilePath);
                PdfDocument pdf = new(writer);
                // Letter page size
                PageSize pageSize = PageSize.LETTER;
                // Set page size
                pdf.SetDefaultPageSize(pageSize);

                Document document = new(pdf);
                // Set the custom margins to 80 units on the sides and 55 units on the top and bottom of the document
                document.SetMargins(55, 80, 55, 80);

                //Set Font to Arial (HELVETICA)
                PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
                PdfFont fontBold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

                // Define style for the title
                Style titleStyle = new Style()
                    .SetFont(fontBold)
                    .SetFontSize(12)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetMarginBottom(11 / 2);

                // Define style for the text
                Style paragraphStyle = new Style()
                    .SetFont(font)
                    .SetFontSize(11)
                    .SetTextAlignment(TextAlignment.JUSTIFIED)
                    .SetMarginBottom(11 / 2);

                // Load the bank logo
                ImageData imageData = ImageDataFactory.Create("https://i.postimg.cc/T3Hsx2hq/bankLogo.png");
                Image logoImage = new Image(imageData);

                // Key Words of the contract that will be used in the introduction with Bold Font
                Text bank = new Text("Banco").SetFont(fontBold);
                Text debtor = new Text("Deudor").SetFont(fontBold);
                Text parts = new Text("Partes").SetFont(fontBold);
                Text part = new Text("Parte").SetFont(fontBold);

                // Document Header
                // Add the logo to the document
                document.Add(logoImage.ScaleAbsolute(100f, 50f).SetHorizontalAlignment(HorizontalAlignment.CENTER));
                document.Add(new Paragraph("\n"));

                // Document Title
                document.Add(new Paragraph("Contrato de Renegociación de Préstamo Bancario").AddStyle(titleStyle));
                //Documento Date with TextAlignment.LEFT
                document.Add(new Paragraph("\n"));
                document.Add(new Paragraph(date).AddStyle(paragraphStyle).SetTextAlignment(TextAlignment.LEFT));
                document.Add(new Paragraph("\n"));

                // Contract Introduction
                Paragraph paragraph = new();
                paragraph.Add(new Text("ALPHA BANK").SetFont(fontBold));
                paragraph.Add(", una institución bancaria costarricense registrada ante la Superintendencia General de Entidades" +
                              " Financieras e inscrita en el Banco Central de Costa Rica identificada con la cedula jurídica 3-000-999999," +
                              " con domicilio legal en San José, Escazú, San Rafael, Edificio Bancario y su Representante Legal es la ");
                paragraph.Add(new Text("Sra. Ms. OLDA BUSTILLOS ORTEGA").SetFont(fontBold));
                paragraph.Add(", portadora de la cédula de identidad 3-1111-1111; será llamada en el presente contrato como el “");
                paragraph.Add(bank);
                paragraph.Add("”.");
                document.Add(paragraph.AddStyle(paragraphStyle));

                paragraph = new();
                paragraph.Add(new Text(clientFullName.ToUpper()).SetFont(fontBold));
                paragraph.Add(" de número de Identificación " + identificationType + " " + identificationNumber + " con domicilio en " + address + " en adelante denominado el “");
                paragraph.Add(debtor);
                paragraph.Add("”.");
                document.Add(paragraph.AddStyle(paragraphStyle));

                paragraph = new Paragraph();
                paragraph.Add("El ");
                paragraph.Add(bank);
                paragraph.Add(" y el ");
                paragraph.Add(debtor);
                paragraph.Add(" se mencionarán conjuntamente como las “");
                paragraph.Add(parts);
                paragraph.Add("” y cada uno será una “");
                paragraph.Add(part);
                paragraph.Add("”.");
                document.Add(paragraph.AddStyle(paragraphStyle));

                // Loan agreements
                document.Add(new Paragraph("Las Partes acuerdan la siguiente renegociación del contrato de préstamo de Número de Identificación " + loandID + ":").AddStyle(paragraphStyle));

                document.Add(new Paragraph("1.Modificación del Monto del Préstamo:").AddStyle(paragraphStyle).SetMarginLeft(25));
                // The Loan Amount is formated to Money Format using the method MoneyFormat() also we get the LoanAmount into words using .ToWords() from the Humanizer Library
                document.Add(new Paragraph("i. El Banco y el Deudor acuerdan modificar el monto total del préstamo original de " + MoneyFormatHelper.MoneyFormat(loanAmount.ToString(), currency) + " (" + ((long)loanAmount).ToWords(spanish) + ") a un nuevo monto de " + MoneyFormatHelper.MoneyFormat(newLoanAmount.ToString(), currency) + " ("  + ((long)newLoanAmount).ToWords(spanish) + ").").AddStyle(paragraphStyle).SetMarginLeft(50));

                document.Add(new Paragraph("2. Modificación de la Tasa de Interés:").AddStyle(paragraphStyle).SetMarginLeft(25));
                document.Add(new Paragraph("i. La tasa de interés acordada para este préstamo será ajustada a " + newInterestPercentage + "% anual.").AddStyle(paragraphStyle).SetMarginLeft(50));

                document.Add(new Paragraph("3. Modificación del Plazo del Préstamo:").AddStyle(paragraphStyle).SetMarginLeft(25));
                document.Add(new Paragraph("i. El plazo de este préstamo será extendido a " + newloanTerm + " meses, comenzando a partir de la fecha de firma de este contrato de renegociación.").AddStyle(paragraphStyle).SetMarginLeft(50));

                document.Add(new Paragraph("4. Modificación de la Forma de Pago").AddStyle(paragraphStyle).SetMarginLeft(25));
                document.Add(new Paragraph("i. El Deudor deberá realizar pagos mensuales por el monto de " + MoneyFormatHelper.MoneyFormat(newPaymentAmount.ToString(), currency) + " (más intereses) en las fechas acordadas entre Las Partes.").AddStyle(paragraphStyle).SetMarginLeft(50));

                document.Add(new Paragraph("5. Penalizaciones por Pagos Atrasados:").AddStyle(paragraphStyle).SetMarginLeft(25));
                document.Add(new Paragraph("i. En caso de que el Deudor no realice los pagos en las fechas acordadas, se aplicará una penalización sobre el monto adeudado por cada día de retraso, de acuerdo con lo estipulado en el contrato original.").AddStyle(paragraphStyle).SetMarginLeft(50));

                document.Add(new Paragraph("6. Uso del Préstamo:").AddStyle(paragraphStyle).SetMarginLeft(25));
                document.Add(new Paragraph("i. El Deudor continuará utilizando el monto del préstamo exclusivamente con el propósito original de " + description + ", como se especifica en el contrato original.").AddStyle(paragraphStyle).SetMarginLeft(50));

                document.Add(new Paragraph("7. Vencimiento Anticipado:").AddStyle(paragraphStyle).SetMarginLeft(25));
                document.Add(new Paragraph("i. Las disposiciones sobre el vencimiento anticipado del préstamo, establecidas en el contrato original, permanecerán vigentes en este acuerdo de renegociación.").AddStyle(paragraphStyle).SetMarginLeft(50));

                document.Add(new Paragraph("8. Ley Aplicable:").AddStyle(paragraphStyle).SetMarginLeft(25));
                document.Add(new Paragraph("i. Este contrato de renegociación se regirá e interpretará de acuerdo con las leyes de la República de Costa Rica, al igual que el contrato original.").AddStyle(paragraphStyle).SetMarginLeft(50));

                document.Add(new Paragraph("9. Firma de las Partes:").AddStyle(paragraphStyle).SetMarginLeft(25));
                document.Add(new Paragraph("i. Las partes reconocen haber leído, entendido y aceptado todas las cláusulas de este contrato de renegociación y lo firman en señal de conformidad:").AddStyle(paragraphStyle).SetMarginLeft(50));

                document.Add(new Paragraph("\n\nPor el Banco Alpha Bank").AddStyle(paragraphStyle));
                document.Add(new Paragraph("\n____________________________").AddStyle(paragraphStyle));
                document.Add(new Paragraph("Firma del Representante del Banco").AddStyle(paragraphStyle));

                document.Add(new Paragraph("\n\n" + clientFullName).AddStyle(paragraphStyle));
                document.Add(new Paragraph("\n____________________________").AddStyle(paragraphStyle));
                document.Add(new Paragraph("Firma del Deudor").AddStyle(paragraphStyle));

                document.Close();

                // If the creation was succesfully the finalFilePath of the new PDF Contract File is returned.
                return finalFilePath;
            }
            catch (Exception)
            {
                // An error occurred, so the fiinalFilePath is ""
                return "";
            }

        }
    }
}
