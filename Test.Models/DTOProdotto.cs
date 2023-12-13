using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Models
{
    public class DTOProdotto
    {
        [Key] public string Nome { get; set; }
        public string Categoria { get; set; }
        public decimal? Costo_produzione { get; set; }
    }
}
