using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace AziendaGestionale.Models.DTO
{
    public class FatturaDTO
    {
        [Key] public string Id_fattura { get; set; }
        public string Id_venditore { get; set; }
        public string Id_cliente { get; set; }
        [Key] public DateTime Data_vendita { get; set; }
        public float? Totale { get; set; }

        private string Data => Data_vendita.ToShortDateString();
      

        
    }
}
