using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Models.FileHelpers
{
    [DelimitedRecord("|")]
    public class FHCliente
    {
        public string Id_cliente { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string? Citta { get; set; }

        public FHCliente() { }
    }
}
