using FileHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Models.FileHelpers
{
    [DelimitedRecord("|")]
    public class FHFattura
    {
        public string Id_fattura { get; set; }
        public string Id_venditore { get; set; }
        public string Id_cliente { get; set; }
        public DateTime Data_vendita { get; set; }
        public float? Totale { get; set; }

        /*
        public FHFattura(DTOFattura fattua) 
        { 
            Id_fattura = fattua.Id_fattura;
            Id_cliente = fattua.Id_cliente;
            Id_venditore = fattua.Id_venditore;
            Data_vendita = fattua.Data_vendita;
            Totale = fattua.Totale;
        }
        */

        public FHFattura() { }
    }
}
