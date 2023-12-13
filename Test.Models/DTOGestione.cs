using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Models
{
    public class DTOGestione
    {
        [Key] public string Id_dipendente { get; set; }
        [Key] public DateTime Data_assegnazione { get; set; }
        public string Settore { get; set; }
        public string? Categoria { get; set; }
    }
}
