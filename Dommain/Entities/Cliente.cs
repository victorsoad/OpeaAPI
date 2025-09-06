using Opea.Domain.Enums;

namespace Opea.Domain.Entities
{
    // A entidade de domínio Cliente. Removemos as DataAnnotations para
    // usar a Fluent API, deixando a classe mais limpa e focada no domínio.
    public class Cliente
    {
        // O Id do cliente, que será a chave primária.
        public Guid Id { get; private set; }

        // O nome da empresa, sem atributos de validação.
        public string NomeEmpresa { get; private set; }

        // O porte da empresa, usando a enumeração definida.
        public PorteEmpresa PorteEmpresa { get; private set; }

        // Construtor vazio para o Entity Framework Core.
        public Cliente() { }

        // Construtor principal para criar um novo cliente com os dados fornecidos.
        public Cliente(string nomeEmpresa, PorteEmpresa porteEmpresa)
        {
            if (string.IsNullOrWhiteSpace(nomeEmpresa))
                throw new ArgumentException("Nome da empresa não pode ser nulo ou vazio", nameof(nomeEmpresa));

            Id = Guid.NewGuid();
            NomeEmpresa = nomeEmpresa;
            PorteEmpresa = porteEmpresa;
        }

        // Método para atualizar o cliente, garantindo a imutabilidade do Id.
        public void Update(string nomeEmpresa, PorteEmpresa porteEmpresa)
        {
            if (string.IsNullOrWhiteSpace(nomeEmpresa))
                throw new ArgumentException("Nome da empresa não pode ser nulo ou vazio", nameof(nomeEmpresa));

            NomeEmpresa = nomeEmpresa;
            PorteEmpresa = porteEmpresa;
        }
    }
}
