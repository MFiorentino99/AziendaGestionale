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

            if (recordLine.Contains("|")) 
            {
                if (Char.IsLetter(recordLine[0]))
                {
                    return typeof(FHFatturaReadingDelimited);
                }
                else
                {
                    return typeof(FHClienteReadingDelimited);
                }
            }
            else
            {
                if (Char.IsLetter(recordLine[0]))
                {
                    return typeof(FHFatturaReadingFixed);

                }
                else
                {
                    return typeof(FHClienteReadingFixed);
                }
            }
        }
    }
}
