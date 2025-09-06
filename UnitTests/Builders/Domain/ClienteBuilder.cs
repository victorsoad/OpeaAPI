using Bogus;
using Opea.Domain.Entities;
using Opea.Domain.Enums;

namespace Opea.Tests.Builders.Domain
{
    /// <summary>
    /// Builder para criar instâncias de Cliente para testes usando o padrão Builder
    /// </summary>
    public class ClienteBuilder
    {
        private readonly Faker _faker;
        private Guid _id;
        private string _nomeEmpresa;
        private PorteEmpresa _porteEmpresa;

        public ClienteBuilder()
        {
            _faker = new Faker("pt_BR");
            _id = Guid.NewGuid();
            _nomeEmpresa = _faker.Company.CompanyName();
            _porteEmpresa = _faker.PickRandom<PorteEmpresa>();
        }

        /// <summary>
        /// Cria uma nova instância do ClienteBuilder
        /// </summary>
        public static ClienteBuilder Novo() => new();

        /// <summary>
        /// Define o ID do cliente
        /// </summary>
        public ClienteBuilder ComId(Guid id)
        {
            _id = id;
            return this;
        }

        /// <summary>
        /// Define o nome da empresa
        /// </summary>
        public ClienteBuilder ComNomeEmpresa(string nomeEmpresa)
        {
            _nomeEmpresa = nomeEmpresa;
            return this;
        }

        /// <summary>
        /// Define o porte da empresa
        /// </summary>
        public ClienteBuilder ComPorteEmpresa(PorteEmpresa porteEmpresa)
        {
            _porteEmpresa = porteEmpresa;
            return this;
        }

        /// <summary>
        /// Define um nome de empresa pequena
        /// </summary>
        public ClienteBuilder ComoPequenaEmpresa()
        {
            _nomeEmpresa = _faker.Company.CompanyName() + " LTDA";
            _porteEmpresa = PorteEmpresa.Pequena;
            return this;
        }

        /// <summary>
        /// Define um nome de empresa média
        /// </summary>
        public ClienteBuilder ComoMediaEmpresa()
        {
            _nomeEmpresa = _faker.Company.CompanyName() + " S.A.";
            _porteEmpresa = PorteEmpresa.Media;
            return this;
        }

        /// <summary>
        /// Define um nome de empresa grande
        /// </summary>
        public ClienteBuilder ComoGrandeEmpresa()
        {
            _nomeEmpresa = _faker.Company.CompanyName() + " CORPORATION";
            _porteEmpresa = PorteEmpresa.Grande;
            return this;
        }

        /// <summary>
        /// Define um nome de empresa com caracteres especiais para testar validação
        /// </summary>
        public ClienteBuilder ComNomeInvalido()
        {
            _nomeEmpresa = _faker.Random.String(101); // Nome muito longo
            return this;
        }

        /// <summary>
        /// Define um nome de empresa vazio para testar validação
        /// </summary>
        public ClienteBuilder ComNomeVazio()
        {
            _nomeEmpresa = string.Empty;
            return this;
        }

        /// <summary>
        /// Constrói a instância do Cliente
        /// </summary>
        public Cliente Build()
        {
            return new Cliente(_nomeEmpresa, _porteEmpresa);
        }

        /// <summary>
        /// Constrói uma lista de clientes
        /// </summary>
        public List<Cliente> BuildList(int quantidade = 5)
        {
            return _faker.Make(quantidade, () => Build()).ToList();
        }

        /// <summary>
        /// Constrói um cliente com dados aleatórios válidos
        /// </summary>
        public Cliente BuildValido()
        {
            return new ClienteBuilder()
                .ComNomeEmpresa(_faker.Company.CompanyName())
                .ComPorteEmpresa(_faker.PickRandom<PorteEmpresa>())
                .Build();
        }
    }
}
