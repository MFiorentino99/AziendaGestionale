using System.ComponentModel.DataAnnotations;

namespace AziendaGestionale.Models.Entities
{
    public class Gestione
    {
        [Key] public string Id_dipendente { get; set; }
        [Key] public DateTime Data_assegnazione { get; set; }
        public string Settore { get; set; }
        public string? Categoria { get; set; }
    }
}
