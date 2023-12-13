using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Models
{
    public class DTOCliente
    {
        [Key] public string Id_cliente { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string? Citta { get; set; }
    }
}
