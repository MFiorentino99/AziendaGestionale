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
            
            if (Char.IsLetter(recordLine[5]) || Char.IsDigit(recordLine[5])) 
            {
                if (engine.Options.FieldsNames[0] == "Id_cliente")
                {
                    return typeof(FHClienteReadingFixed);
                }
                else
                {
                    return typeof(FHFatturaReadingFixed);
                }
            }
            else
            {
                if (engine.Options.FieldsNames[0] == "Id_cliente")
                {
                    return typeof(FHClienteReadingDelimited);
                }
                else
                {
                    return typeof(FHFatturaReadingDelimited);
                }
            }
            /*

            if (engine.Options.FieldsNames[0] == "Id_cliente")
            {
                return typeof(FHClienteReadingFixed);
            }else
            {
                return typeof(FHFatturaReadingFixed);
            }
            */
        }
    }
}
