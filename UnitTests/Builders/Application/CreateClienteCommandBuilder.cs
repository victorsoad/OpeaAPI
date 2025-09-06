using Bogus;
using Opea.Application.Commands;
using Opea.Domain.Enums;

namespace Opea.Tests.Builders.Application
{
    /// <summary>
    /// Builder para criar instâncias de CreateClienteCommand para testes usando o padrão Builder
    /// </summary>
    public class CreateClienteCommandBuilder
    {
        private readonly Faker _faker;
        private string _nomeEmpresa;
        private PorteEmpresa _porteEmpresa;

        public CreateClienteCommandBuilder()
        {
            _faker = new Faker("pt_BR");
            _nomeEmpresa = _faker.Company.CompanyName();
            _porteEmpresa = _faker.PickRandom<PorteEmpresa>();
        }

        /// <summary>
        /// Cria uma nova instância do CreateClienteCommandBuilder
        /// </summary>
        public static CreateClienteCommandBuilder Novo() => new();

        /// <summary>
        /// Define o nome da empresa
        /// </summary>
        public CreateClienteCommandBuilder ComNomeEmpresa(string nomeEmpresa)
        {
            _nomeEmpresa = nomeEmpresa;
            return this;
        }

        /// <summary>
        /// Define o porte da empresa
        /// </summary>
        public CreateClienteCommandBuilder ComPorteEmpresa(PorteEmpresa porteEmpresa)
        {
            _porteEmpresa = porteEmpresa;
            return this;
        }

        /// <summary>
        /// Define um comando para empresa pequena
        /// </summary>
        public CreateClienteCommandBuilder ParaPequenaEmpresa()
        {
            _nomeEmpresa = _faker.Company.CompanyName() + " LTDA";
            _porteEmpresa = PorteEmpresa.Pequena;
            return this;
        }

        /// <summary>
        /// Define um comando para empresa média
        /// </summary>
        public CreateClienteCommandBuilder ParaMediaEmpresa()
        {
            _nomeEmpresa = _faker.Company.CompanyName() + " S.A.";
            _porteEmpresa = PorteEmpresa.Media;
            return this;
        }

        /// <summary>
        /// Define um comando para empresa grande
        /// </summary>
        public CreateClienteCommandBuilder ParaGrandeEmpresa()
        {
            _nomeEmpresa = _faker.Company.CompanyName() + " CORPORATION";
            _porteEmpresa = PorteEmpresa.Grande;
            return this;
        }

        /// <summary>
        /// Define um nome de empresa inválido (muito longo)
        /// </summary>
        public CreateClienteCommandBuilder ComNomeInvalido()
        {
            _nomeEmpresa = _faker.Random.String(101); // Nome muito longo
            return this;
        }

        /// <summary>
        /// Define um nome de empresa vazio
        /// </summary>
        public CreateClienteCommandBuilder ComNomeVazio()
        {
            _nomeEmpresa = string.Empty;
            return this;
        }

        /// <summary>
        /// Define um nome de empresa nulo
        /// </summary>
        public CreateClienteCommandBuilder ComNomeNulo()
        {
            _nomeEmpresa = null!;
            return this;
        }

        /// <summary>
        /// Constrói a instância do CreateClienteCommand
        /// </summary>
        public CreateClienteCommand Build()
        {
            return new CreateClienteCommand(_nomeEmpresa, _porteEmpresa);
        }

        /// <summary>
        /// Constrói um comando com dados aleatórios válidos
        /// </summary>
        public CreateClienteCommand BuildValido()
        {
            return new CreateClienteCommandBuilder()
                .ComNomeEmpresa(_faker.Company.CompanyName())
                .ComPorteEmpresa(_faker.PickRandom<PorteEmpresa>())
                .Build();
        }
    }
}
