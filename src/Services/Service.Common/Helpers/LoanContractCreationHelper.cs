using Data.AlphaBank;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.Globalization;

using static System.Net.Mime.MediaTypeNames;
using Text = iText.Layout.Element.Text;
using Document = iText.Layout.Document;
using iText.Bouncycastle.Crypto;
using Humanizer;

namespace Service.Common.Helpers
{
    internal static class LoanContractCreationHelper
    {
        private const string filePath = @"C:\Documents\Proyecto-SoftwareIII\Contratos";

        public static string CreatePdf(LoanApplication oLoanApplication)
        {

            var spanish = new CultureInfo("es-CR");

            string currency = oLoanApplication.TypeCurrency.Description;
            string date = DateTime.Now.ToString("dd 'de' MMMM 'del' yyyy", spanish);
            string clientFullName = oLoanApplication.Account.Customer.Person.Name + " " + oLoanApplication.Account.Customer.Person.FirstName + " " + oLoanApplication.Account.Customer.Person.SecondName;
            string identificationType = oLoanApplication.Account.Customer.Person.TypeIdentification.Description;

            string identificationNumber;
            switch (oLoanApplication.Account.Customer.Person.TypeIdentificationId)
            {
                case 1:
                    identificationNumber = oLoanApplication.Account.Customer.PersonId.ToString("#-####-####");
                    break;
                case 2:
                    identificationNumber = oLoanApplication.Account.Customer.PersonId.ToString("#-###-######");
                    break;
                default:
                    identificationNumber = oLoanApplication.Account.Customer.PersonId.ToString();
                    break;
            }

            string address = oLoanApplication.Account.Customer.Person.Address;
            decimal loanAmount = oLoanApplication.Amount;
            string interestPercentage = oLoanApplication.Interest.Description;
            string loanTerm = oLoanApplication.Deadline.Description.Split(' ')[0];
            decimal paymentAmount = (loanAmount / decimal.Parse(loanTerm)) + (loanAmount * (decimal.Parse(interestPercentage) / 100));
            string description = oLoanApplication.Justification;

            string finalFilePath = System.IO.Path.Combine(filePath, oLoanApplication.Id + "_loan_contract_" + oLoanApplication.Account.Customer.PersonId + ".pdf");


            try
            {
                PdfWriter writer = new PdfWriter(finalFilePath);
                PdfDocument pdf = new PdfDocument(writer);
                PageSize pageSize = PageSize.LETTER; // Tamaño de página carta
                pdf.SetDefaultPageSize(pageSize); // Establecer el tamaño de página

                Document document = new Document(pdf);
                document.SetMargins(65, 80, 65, 80); // Establecer los márgenes personalizados en 80 unidades = 1 pulgada

                //Set Font to Arial (HELVETICA)
                PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
                PdfFont fontBold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

                // Definir estilo para el título
                Style titleStyle = new Style()
                    .SetFont(fontBold)
                    .SetFontSize(12)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetMarginBottom(11 / 2);

                // Definir estilo para el texto
                Style paragraphStyle = new Style()
                    .SetFont(font)
                    .SetFontSize(11)
                    .SetTextAlignment(TextAlignment.JUSTIFIED)
                    .SetMarginBottom(11 / 2);

                Text bank = new Text("Banco").SetFont(fontBold);
                Text debtor = new Text("Deudor").SetFont(fontBold);
                Text parts = new Text("Partes").SetFont(fontBold);
                Text part = new Text("Parte").SetFont(fontBold);


                document.Add(new Paragraph("Contrato de Otorgamiento de Préstamo Bancario").AddStyle(titleStyle));
                document.Add(new Paragraph("\n"));
                document.Add(new Paragraph(date).AddStyle(paragraphStyle).SetTextAlignment(TextAlignment.LEFT));
                document.Add(new Paragraph("\n"));

                Paragraph paragraph = new Paragraph();
                paragraph.Add(new Text("ALPHA BANK").SetFont(fontBold));
                paragraph.Add(", una institución bancaria costarricense registrada ante la Superintendencia General de Entidades" +
                              " Financieras e inscrita en el Banco Central de Costa Rica identificada con la cedula jurídica 3-000-999999," +
                              " con domicilio legal en San José, Escazú, San Rafael, Edificio Bancario y su Representante Legal es la ");
                paragraph.Add(new Text("Sra. Ms. OLDA BUSTILLOS ORTEGA").SetFont(fontBold));
                paragraph.Add(", portadora de la cédula de identidad 3-1111-1111; será llamada en el presente contrato como el “");
                paragraph.Add(bank);
                paragraph.Add("”.");
                document.Add(paragraph.AddStyle(paragraphStyle));

                paragraph = new Paragraph();
                paragraph.Add(new Text(clientFullName.ToUpper()).SetFont(fontBold));
                paragraph.Add(" de identificación " + identificationType + " " + identificationNumber + " con domicilio en " + address + " en adelante denominado el “");
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

                document.Add(new Paragraph("Las Partes acuerdan el siguiente contrato de préstamo:").AddStyle(paragraphStyle));

                document.Add(new Paragraph("1. Monto del Préstamo:").AddStyle(paragraphStyle).SetMarginLeft(25));
                document.Add(new Paragraph("i. El Banco otorga al Deudor un préstamo por un monto total de ₡" + MoneyFormat(loanAmount.ToString(), currency) + " (" + long.Parse(loanAmount.ToString()).ToWords(spanish) + ").").AddStyle(paragraphStyle).SetMarginLeft(50));

                document.Add(new Paragraph("2. Tasa de Interés:").AddStyle(paragraphStyle).SetMarginLeft(25));
                document.Add(new Paragraph("i. La tasa de interés acordada para este préstamo es del " + interestPercentage + "% anual.").AddStyle(paragraphStyle).SetMarginLeft(50));

                document.Add(new Paragraph("3. Plazo del Préstamo:").AddStyle(paragraphStyle).SetMarginLeft(25));
                document.Add(new Paragraph("i. El plazo de este préstamo es de " + loanTerm + " meses, comenzando a partir de la fecha de desembolso del préstamo.").AddStyle(paragraphStyle).SetMarginLeft(50));

                document.Add(new Paragraph("4. Forma de Pago:").AddStyle(paragraphStyle).SetMarginLeft(25));
                document.Add(new Paragraph("i. El Deudor deberá realizar pagos mensuales por el monto de " + MoneyFormat(paymentAmount.ToString(), currency) + " en las fechas acordadas entre Las Partes.").AddStyle(paragraphStyle).SetMarginLeft(50));

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

                return finalFilePath;
            }
            catch (Exception)
            {
                return "";
            }
        }

        static string MoneyFormat(string amount, string currecncy)
        {
            decimal cantidadDecimal = decimal.Parse(amount);
            CultureInfo culture;

            switch (currecncy)
            {
                case "USD":
                    culture = CultureInfo.GetCultureInfo("en-US");
                    break;
                case "CRC":
                    culture = CultureInfo.GetCultureInfo("es-CR");
                    break;
                case "EUR":
                    culture = CultureInfo.GetCultureInfo("es-ES");
                    break;
                default:
                    culture = CultureInfo.InvariantCulture;
                    break;
            }

            return cantidadDecimal.ToString("C", culture).Replace(culture.NumberFormat.CurrencySymbol, "") + " " + currecncy;
        }
    }
}
