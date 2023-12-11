using System.ComponentModel.DataAnnotations;

namespace AziendaGestionale.Models.DTO
{
    public class DettaglioDTO
    {
     
        [Key] public string Id_fattura { get; set; }
        [Key] public string Prodotto { get; set; }

        public int Quantita { get; set; }
        public decimal Costo { get; set; }
    }
}
