using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models.FileHelpers;

namespace Test.Models
{
    public class Selector
    {
        public Type CustomSelector(MultiRecordEngine engine,string recordLine)
        {
            if(recordLine.Length == 0)
            {
                return null;
            }
            if (Char.IsNumber(recordLine[0]))
            {
                return typeof(FHClienteReading);
            }
            else
            {
                return typeof(FHFatturaReading);
            }
        }
    }
}
