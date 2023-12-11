using AziendaGestionale.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AziendaGestionale.Models
{
    public class Context:DbContext
    {
        public Context(DbContextOptions<Context> options)
           : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dettaglio>().HasKey(d => new { d.Id_fattura, d.Prodotto });
            modelBuilder.Entity<Fattura>().HasKey(f => new { f.Id_fattura, f.Data_vendita });
            modelBuilder.Entity<Gestione>().HasKey(g => new { g.Id_dipendente, g.Data_assegnazione });

        }
        public DbSet<AziendaGestionale.Models.Entities.Cliente>? Cliente { get; set; }
        public DbSet<AziendaGestionale.Models.Entities.Dettaglio>? Dettaglio { get; set; }
        public DbSet<AziendaGestionale.Models.Entities.Dipendente>? Dipendente { get; set; }
        public DbSet<AziendaGestionale.Models.Entities.Fattura>? Fattura { get; set; }
        
        public DbSet<AziendaGestionale.Models.Entities.Prodotto>? Prodotto { get; set; }
        
        public DbSet<AziendaGestionale.Models.Entities.Gestione>? Gestione { get; set; }
        
      
    }
}
