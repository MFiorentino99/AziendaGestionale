using FileHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Models
{
    public class DTOFattura
    {
        [Key] public string Id_fattura { get; set; }
        public string Id_venditore { get; set; }
        public string Id_cliente { get; set; }
        [Key] public DateTime Data_vendita { get; set; }
        public float? Totale { get; set; }
    }
}
