using System.Globalization;

namespace Service.Common.Helpers
{
    public static class MoneyFormatHelper
    {

        // This method is used to change amounts to money format depending of the currency
        public static string MoneyFormat(string amount, string currecncy)
        {
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
