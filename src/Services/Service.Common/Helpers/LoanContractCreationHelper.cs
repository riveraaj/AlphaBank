using Data.AlphaBank;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.Globalization;
using Text = iText.Layout.Element.Text;
using Document = iText.Layout.Document;
using Humanizer;
using iText.IO.Image;

namespace Service.Common.Helpers {
    internal static class LoanContractCreationHelper {

        private const string filePath = @"C:\Documents\Proyecto-SoftwareIII\Contratos";

        public static string CreatePdf(LoanApplication oLoanApplication) {
            // If the folders doesn´t exists, then they will be created
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);          

            //Get the CultureInfo from Costa Rica because this will be used by Humanizer Library
            var spanish = new CultureInfo("es-CR");

            //Get the currency description of the LoanApplication
            string currency = oLoanApplication.TypeCurrency.Description;
            //Get the todays date in string format
            string date = DateTime.Now.ToString("dd 'de' MMMM 'del' yyyy", spanish);
            //Get the Name, FirstName and SecondName of the Customer Owner of the Account and LoanApplication, then concatenate them into the full name
            string clientFullName = oLoanApplication.Account.Customer.Person.Name + " " 
                + oLoanApplication.Account.Customer.Person.FirstName + " " 
                + oLoanApplication.Account.Customer.Person.SecondName;

            //Get the TypeIdentification Description of the Customer(Person)
            string identificationType = oLoanApplication.Account.Customer.Person.TypeIdentification.Description;

            //The IdentificationNumber will be StringFormated based on the TypeIdentificationId
            string identificationNumber = oLoanApplication.Account.Customer.Person.TypeIdentificationId switch {
                        1 => oLoanApplication.Account.Customer.PersonId.ToString("#-####-####"),// TypeIdentificationId 1 is for "Física" so the format will be assing to the string
                        2 => oLoanApplication.Account.Customer.PersonId.ToString("#-###-######"),// TypeIdentificationId 2 is for "Jurídica" so the format will be assing to the string
                        _ => oLoanApplication.Account.Customer.PersonId.ToString(),// Default format
                    };

            //Get the address of the Customer(Person)
            string address = oLoanApplication.Account.Customer.Person.Address;
            //Get the loan requested amount of the LoanApplication
            decimal loanAmount = oLoanApplication.Amount;
            //Get the interest description of the LoanApplication
            string interestPercentage = oLoanApplication.Interest.Description;
            // Get the loanTerm, taking on consideration that the DeadLine description is in  format "# meses" 
            // the description will be splited by " ". Then we only get the first element of the list, so it will be "#". 
            string loanTerm = oLoanApplication.Deadline.Description.Split(' ')[0];
            // Get the PaymentAmount of the Loan dividing the LoanAmount by the LoanTerm (this amount doesn´t include interests)
            decimal paymentAmount = (loanAmount / decimal.Parse(loanTerm));
            // Get the description or reason of the LoanApplication
            string description = oLoanApplication.Justification;

            // Combine the filePath(Where the contracts will be stored) with the pdf File Name (LoanApplicationId_loan_contract_PersonId.pdf)
            string finalFilePath = System.IO.Path.Combine(filePath, oLoanApplication.Id + "_loan_contract_" 
                + oLoanApplication.Account.Customer.PersonId + ".pdf");

            try  {
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
                document.Add(new Paragraph("Contrato de Otorgamiento de Préstamo Bancario").AddStyle(titleStyle));
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
                document.Add(new Paragraph("Las Partes acuerdan el siguiente contrato de préstamo:").AddStyle(paragraphStyle));

                document.Add(new Paragraph("1. Monto del Préstamo:").AddStyle(paragraphStyle).SetMarginLeft(25));
                // The Loan Amount is formated to Money Format using the method MoneyFormat() also we get the LoanAmount into words using .ToWords() from the Humanizer Library
                document.Add(new Paragraph("i. El Banco otorga al Deudor un préstamo por un monto total de " + MoneyFormat(loanAmount.ToString(), currency) + " (" + ((long)loanAmount).ToWords(spanish) + ").").AddStyle(paragraphStyle).SetMarginLeft(50));

                document.Add(new Paragraph("2. Tasa de Interés:").AddStyle(paragraphStyle).SetMarginLeft(25));
                document.Add(new Paragraph("i. La tasa de interés acordada para este préstamo es del " + interestPercentage + "% anual.").AddStyle(paragraphStyle).SetMarginLeft(50));

                document.Add(new Paragraph("3. Plazo del Préstamo:").AddStyle(paragraphStyle).SetMarginLeft(25));
                document.Add(new Paragraph("i. El plazo de este préstamo es de " + loanTerm + " meses, comenzando a partir de la fecha de desembolso del préstamo.").AddStyle(paragraphStyle).SetMarginLeft(50));

                document.Add(new Paragraph("4. Forma de Pago:").AddStyle(paragraphStyle).SetMarginLeft(25));
                document.Add(new Paragraph("i. El Deudor deberá realizar pagos mensuales por el monto de " + MoneyFormat(paymentAmount.ToString(), currency) + " (más intereses) en las fechas acordadas entre Las Partes.").AddStyle(paragraphStyle).SetMarginLeft(50));

                document.Add(new Paragraph("5. Penalizaciones por Pagos Atrasados:").AddStyle(paragraphStyle).SetMarginLeft(25));
                document.Add(new Paragraph("i. En caso de que el Deudor no realice los pagos en las fechas acordadas, se aplicará una penalización sobre el monto adeudado por cada día de retraso.").AddStyle(paragraphStyle).SetMarginLeft(50));

                document.Add(new Paragraph("6. Uso del Préstamo:").AddStyle(paragraphStyle).SetMarginLeft(25));
                document.Add(new Paragraph("i. El Deudor utilizará el monto del préstamo exclusivamente por motivo de " + description + ".").AddStyle(paragraphStyle).SetMarginLeft(50));

                document.Add(new Paragraph("7. Vencimiento Anticipado:").AddStyle(paragraphStyle).SetMarginLeft(25));
                document.Add(new Paragraph("i. El Banco se reserva el derecho de dar por vencido anticipadamente el préstamo en caso de incumplimiento por parte del Deudor de cualquiera de las cláusulas de este contrato.").AddStyle(paragraphStyle).SetMarginLeft(50));

                document.Add(new Paragraph("8. Ley Aplicable:").AddStyle(paragraphStyle).SetMarginLeft(25));
                document.Add(new Paragraph("i. Este contrato se regirá e interpretará de acuerdo con las leyes de la República de Costa Rica.").AddStyle(paragraphStyle).SetMarginLeft(50));

                document.Add(new Paragraph("9. Firma de las Partes:").AddStyle(paragraphStyle).SetMarginLeft(25));
                document.Add(new Paragraph("i. Las partes reconocen haber leído, entendido y aceptado todas las cláusulas de este contrato y lo firman en señal de conformidad:").AddStyle(paragraphStyle).SetMarginLeft(50));

                document.Add(new Paragraph("\n\nPor el Banco Alpha Bank").AddStyle(paragraphStyle));
                document.Add(new Paragraph("\n____________________________").AddStyle(paragraphStyle));
                document.Add(new Paragraph("Firma del Representante del Banco").AddStyle(paragraphStyle));

                document.Add(new Paragraph("\n\n" + clientFullName).AddStyle(paragraphStyle));
                document.Add(new Paragraph("\n____________________________").AddStyle(paragraphStyle));
                document.Add(new Paragraph("Firma del Deudor").AddStyle(paragraphStyle));

                document.Close();

                // If the creation was succesfully the finalFilePath of the new PDF Contract File is returned.
                return finalFilePath;
            } catch (Exception) {
                // An error occurred, so the fiinalFilePath is ""
                return "";
            }
        }

        // This method is used to change amounts to money format depending of the currency
        static string MoneyFormat(string amount, string currecncy) {
            decimal cantidadDecimal = decimal.Parse(amount);

            CultureInfo culture = currecncy switch
            {
                "USD" => CultureInfo.GetCultureInfo("en-US"),
                "CRC" => CultureInfo.GetCultureInfo("es-CR"),
                "EUR" => CultureInfo.GetCultureInfo("es-ES"),
                _ => CultureInfo.InvariantCulture,
            };

            // The amount is converted to money format based on the culture got in the swith.
            // To prevent errors the currency simbol is replaced by "" and then the currency parameter is concatenated
            return cantidadDecimal.ToString("C", culture).Replace(culture.NumberFormat.CurrencySymbol, "") + " " + currecncy;
        }
    }
}