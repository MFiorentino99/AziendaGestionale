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
            try
            {
                return DateTime.ParseExact(str, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            }
            catch (FormatException e)
            {
                return DateTime.Parse("00/00/0000", CultureInfo.InvariantCulture);
            }
        }
    }
}
