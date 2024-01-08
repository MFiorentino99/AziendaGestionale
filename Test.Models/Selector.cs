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
            if (engine.Options.FieldsNames[0] == "Id_cliente")
            {
                return typeof(FHClienteReadingFixed);
            }else
            {
                return typeof(FHFatturaReadingFixed);
            }
        }
    }
}
