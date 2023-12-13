using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Models
{
    public class DTODipendente
    {
        public DTODipendente()
        {
            Stipendio = 0;
        }

        [Key] public string Id_dipendente { get; set; }
        public string? Nome { get; set; }
        public string? Cognome { get; set; }
        public float Stipendio { get; set; }
        public string Settore { get; set; }

        public string? Categoria { get; set; }
    }
}
