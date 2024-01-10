using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Models.FileHelpers
{
    [FixedLengthRecord(FixedMode.AllowLessChars)]
    public class FHFatturaReadingFixed
    {
        [FieldFixedLength(4)]
        public string Id_fattura { get; set; }
        [FieldFixedLength(4)]
        public string Id_venditore { get; set; }
        
        [FieldFixedLength(4)]
        public string Id_cliente { get; set; }

        [FieldFixedLength(10)]
        [FieldConverter(ConverterKind.Date, "dd-MM-yyyy")]
        public DateTime Data_vendita { get; set; }

        [FieldFixedLength(10)]
        public float Totale { get; set; }

        public FHFatturaReadingFixed() { }
    }

    
    [DelimitedRecord("|")]
    public class FHFatturaReadingDelimited
    {

        public string Id_fattura { get; set; }
        public string Id_venditore { get; set; }

        
        public string? Id_cliente { get; set; }
        
        [FieldConverter(ConverterKind.Date, "dd-MM-yyyy")]
        public DateTime Data_vendita { get; set; }

        public float Totale { get; set; }

        public FHFatturaReadingDelimited() { }
    }
    


}
