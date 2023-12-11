using System.ComponentModel.DataAnnotations;

namespace AziendaGestionale.Models.Entities
{
    public class Fattura
    {
        [Key] public string Id_fattura { get; set; }
        public string Id_venditore { get; set; }
        public string Id_cliente { get; set; }
        [Key] public DateTime Data_vendita { get; set; }
        public float? Totale { get; set; }
    }
}
