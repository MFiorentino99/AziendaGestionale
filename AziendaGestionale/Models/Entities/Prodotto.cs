using System.ComponentModel.DataAnnotations;

namespace AziendaGestionale.Models.Entities
{
    public class Prodotto
    {
        [Key] public string Nome { get; set; }
        public string Categoria { get; set; }
        public decimal? Costo_produzione { get; set; }
    }
}
