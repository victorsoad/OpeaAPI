using Bogus;
using Opea.Application.Commands;
using Opea.Domain.Enums;

namespace Opea.Tests.Builders.Application
{
    /// <summary>
    /// Builder para criar instâncias de UpdateClienteCommand para testes usando o padrão Builder
    /// </summary>
    public class UpdateClienteCommandBuilder
    {
        private readonly Faker _faker;
        private Guid _id;
        private string _nomeEmpresa;
        private PorteEmpresa _porteEmpresa;

        public UpdateClienteCommandBuilder()
        {
            _faker = new Faker("pt_BR");
            _id = Guid.NewGuid();
            _nomeEmpresa = _faker.Company.CompanyName();
            _porteEmpresa = _faker.PickRandom<PorteEmpresa>();
        }

        /// <summary>
        /// Cria uma nova instância do UpdateClienteCommandBuilder
        /// </summary>
        public static UpdateClienteCommandBuilder Novo() => new();

        /// <summary>
        /// Define o ID do cliente
        /// </summary>
        public UpdateClienteCommandBuilder ComId(Guid id)
        {
            _id = id;
            return this;
        }

        /// <summary>
        /// Define o nome da empresa
        /// </summary>
        public UpdateClienteCommandBuilder ComNomeEmpresa(string nomeEmpresa)
        {
            _nomeEmpresa = nomeEmpresa;
            return this;
        }

        /// <summary>
        /// Define o porte da empresa
        /// </summary>
        public UpdateClienteCommandBuilder ComPorteEmpresa(PorteEmpresa porteEmpresa)
        {
            _porteEmpresa = porteEmpresa;
            return this;
        }

        /// <summary>
        /// Define um comando para empresa pequena
        /// </summary>
        public UpdateClienteCommandBuilder ParaPequenaEmpresa()
        {
            _nomeEmpresa = _faker.Company.CompanyName() + " LTDA";
            _porteEmpresa = PorteEmpresa.Pequena;
            return this;
        }

        /// <summary>
        /// Define um comando para empresa média
        /// </summary>
        public UpdateClienteCommandBuilder ParaMediaEmpresa()
        {
            _nomeEmpresa = _faker.Company.CompanyName() + " S.A.";
            _porteEmpresa = PorteEmpresa.Media;
            return this;
        }

        /// <summary>
        /// Define um comando para empresa grande
        /// </summary>
        public UpdateClienteCommandBuilder ParaGrandeEmpresa()
        {
            _nomeEmpresa = _faker.Company.CompanyName() + " CORPORATION";
            _porteEmpresa = PorteEmpresa.Grande;
            return this;
        }

        /// <summary>
        /// Define um nome de empresa inválido (muito longo)
        /// </summary>
        public UpdateClienteCommandBuilder ComNomeInvalido()
        {
            _nomeEmpresa = _faker.Random.String(101); // Nome muito longo
            return this;
        }

        /// <summary>
        /// Define um nome de empresa vazio
        /// </summary>
        public UpdateClienteCommandBuilder ComNomeVazio()
        {
            _nomeEmpresa = string.Empty;
            return this;
        }

        /// <summary>
        /// Define um nome de empresa nulo
        /// </summary>
        public UpdateClienteCommandBuilder ComNomeNulo()
        {
            _nomeEmpresa = null!;
            return this;
        }

        /// <summary>
        /// Constrói a instância do UpdateClienteCommand
        /// </summary>
        public UpdateClienteCommand Build()
        {
            return new UpdateClienteCommand(_id, _nomeEmpresa, _porteEmpresa);
        }

        /// <summary>
        /// Constrói um comando com dados aleatórios válidos
        /// </summary>
        public UpdateClienteCommand BuildValido()
        {
            return new UpdateClienteCommandBuilder()
                .ComId(Guid.NewGuid())
                .ComNomeEmpresa(_faker.Company.CompanyName())
                .ComPorteEmpresa(_faker.PickRandom<PorteEmpresa>())
                .Build();
        }
    }
}
