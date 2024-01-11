using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethods
{
    public static class DataTimeExtension
    {
        public static DateTime ConvertFromString(this string str)
        {
            str = str.EliminateDivisor();
            if(str.Length == 8)
            {
                if (!DateTime.TryParseExact(str, "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out var result))
                    throw new FormatException();
                return result;
            }
            else
            {
                throw new ArgumentException();
            }

        }

    }
}
