using System.ComponentModel.DataAnnotations;

namespace AziendaGestionale.Models.Entities
{
    public class Dipendente
    {
        public Dipendente()
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
