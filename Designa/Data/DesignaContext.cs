using Designa.Models;
using Microsoft.EntityFrameworkCore;

namespace Designa.Data
{
    public class DesignaContext : DbContext
    {
        public DesignaContext(DbContextOptions<DesignaContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Publicador> Publicadores { get; set; }
        public DbSet<Reuniao> Reunioes { get; set; }
        public DbSet<Parte> Partes { get; set; }
        public DbSet<PublicadorParte> PublicadorPartes { get; set; }
        public DbSet<PublicadorPrivilegio> PublicadorPrivilegios { get; set; }
        public DbSet<ListaNegra> ListasNegra { get; set; }
    }
}
