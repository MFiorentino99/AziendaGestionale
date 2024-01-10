using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Models.FileHelpers
{
    [FixedLengthRecord(FixedMode.AllowLessChars)]
    public class FHClienteReadingFixed
    {
        [FieldFixedLength(4)]
        public string Id_cliente { get; set; }
        [FieldFixedLength(20)]
        public string Nome { get; set; }
        [FieldFixedLength(20)]
        public string Cognome { get; set; }
        [FieldFixedLength(20)]
        public string Citta { get; set; }

        public FHClienteReadingFixed() { }
    }
    
    
    [DelimitedRecord("|")]
    public class FHClienteReadingDelimited
    {
        
        public string Id_cliente { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Citta { get; set; }

        public FHClienteReadingDelimited() { }
    }
    
}
