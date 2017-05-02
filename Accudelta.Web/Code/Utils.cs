using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Accudelta.Web.Code
{
    public class Utils
    {
        public static string StringFromDecimal(decimal number)
        {
            return number.ToString("#,##0.00", CultureInfo.CreateSpecificCulture("es-ES"));
        }

        public static decimal DecimalFromString(string number)
        {
            return string.IsNullOrEmpty(number) ? 0 : Convert.ToDecimal(number, CultureInfo.CreateSpecificCulture("es-ES"));
        }

        public static DateTime DatetimeFromString(string date)
        {
            return DateTime.ParseExact(date, "ddMMyyyy", CultureInfo.InvariantCulture);
        }

        public static string StringFromDatetime(DateTime? date)
        {
            return (date != null ? date.Value.ToShortDateString() : String.Empty);
        }
    }
}