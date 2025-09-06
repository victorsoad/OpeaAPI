using Opea.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Opea.Infrastructure.Data.Configurations
{
    // Esta classe é responsável por configurar o mapeamento da entidade
    // Cliente usando a Fluent API do Entity Framework Core.
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            // Define a chave primária.
            builder.HasKey(c => c.Id);

            // Configura o nome da empresa para ser obrigatório e ter
            // um tamanho máximo de 100 caracteres.
            builder.Property(c => c.NomeEmpresa)
                .IsRequired()
                .HasMaxLength(100);

            // A enumeração será mapeada para um valor numérico.
            builder.Property(c => c.PorteEmpresa)
                .IsRequired();
        }
    }
}
