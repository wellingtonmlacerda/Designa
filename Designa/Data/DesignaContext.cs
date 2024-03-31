using Designa.Models;
using Microsoft.EntityFrameworkCore;

namespace Designa.Data
{
    public class DesignaContext : DbContext
    {
        public DesignaContext(DbContextOptions<DesignaContext> options) : base(options)
        {
        }

        public DbSet<Irmao> Irmaos { get; set; }
        public DbSet<Reuniao> Reunioes { get; set; }
        public DbSet<Parte> Partes { get; set; }
    }
}
