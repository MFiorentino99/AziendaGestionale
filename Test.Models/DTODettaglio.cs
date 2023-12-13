using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Models
{
    public class DTODettaglio
    {
        [Key] public string Id_fattura { get; set; }
        [Key] public string Prodotto { get; set; }
        public int Quantita { get; set; }
        public decimal Costo { get; set; }
    }
}
