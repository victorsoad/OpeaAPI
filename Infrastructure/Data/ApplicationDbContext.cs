using Microsoft.EntityFrameworkCore;
using Opea.Domain.Entities;
using Opea.Infrastructure.Data.Configurations;

namespace Opea.Infrastructure.Data
{
    // O DbContext do Entity Framework Core.
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Aplica as configurações das entidades
            modelBuilder.ApplyConfiguration(new ClienteConfiguration());
        }
    }
}
