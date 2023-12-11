using System.ComponentModel.DataAnnotations;

namespace AziendaGestionale.Models.DTO
{
    public class ClienteDTO
    {
        [Key] public string Id_cliente { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string? Citta { get; set; }
    }
}
