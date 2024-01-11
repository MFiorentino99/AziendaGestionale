using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethods
{
    public static class StringExtension
    {

        public static string EliminateDivisor(this string str)
        {
            var split = str.Split(new char[] { '-','|','/',';' });
            string resp = "";
            foreach (var chunk in split)
            {
                resp+=chunk;
            }
            return resp;
        }
    }
}
