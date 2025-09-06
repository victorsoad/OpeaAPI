using Bogus;
using Opea.Application.DTOs;
using Opea.Domain.Enums;

namespace Opea.Tests.Builders.Application
{
    /// <summary>
    /// Builder para criar instâncias de ClienteDto para testes usando o padrão Builder
    /// </summary>
    public class ClienteDtoBuilder
    {
        private readonly Faker _faker;
        private Guid _id;
        private string _nomeEmpresa;
        private PorteEmpresa _porteEmpresa;

        public ClienteDtoBuilder()
        {
            _faker = new Faker("pt_BR");
            _id = Guid.NewGuid();
            _nomeEmpresa = _faker.Company.CompanyName();
            _porteEmpresa = _faker.PickRandom<PorteEmpresa>();
        }

        /// <summary>
        /// Cria uma nova instância do ClienteDtoBuilder
        /// </summary>
        public static ClienteDtoBuilder Novo() => new();

        /// <summary>
        /// Define o ID do cliente
        /// </summary>
        public ClienteDtoBuilder ComId(Guid id)
        {
            _id = id;
            return this;
        }

        /// <summary>
        /// Define o nome da empresa
        /// </summary>
        public ClienteDtoBuilder ComNomeEmpresa(string nomeEmpresa)
        {
            _nomeEmpresa = nomeEmpresa;
            return this;
        }

        /// <summary>
        /// Define o porte da empresa
        /// </summary>
        public ClienteDtoBuilder ComPorteEmpresa(PorteEmpresa porteEmpresa)
        {
            _porteEmpresa = porteEmpresa;
            return this;
        }

        /// <summary>
        /// Define um cliente pequeno
        /// </summary>
        public ClienteDtoBuilder ComoPequenaEmpresa()
        {
            _nomeEmpresa = _faker.Company.CompanyName() + " LTDA";
            _porteEmpresa = PorteEmpresa.Pequena;
            return this;
        }

        /// <summary>
        /// Define um cliente médio
        /// </summary>
        public ClienteDtoBuilder ComoMediaEmpresa()
        {
            _nomeEmpresa = _faker.Company.CompanyName() + " S.A.";
            _porteEmpresa = PorteEmpresa.Media;
            return this;
        }

        /// <summary>
        /// Define um cliente grande
        /// </summary>
        public ClienteDtoBuilder ComoGrandeEmpresa()
        {
            _nomeEmpresa = _faker.Company.CompanyName() + " CORPORATION";
            _porteEmpresa = PorteEmpresa.Grande;
            return this;
        }

        /// <summary>
        /// Constrói a instância do ClienteDto
        /// </summary>
        public ClienteDto Build()
        {
            return new ClienteDto
            {
                Id = _id,
                NomeEmpresa = _nomeEmpresa,
                PorteEmpresa = _porteEmpresa
            };
        }

        /// <summary>
        /// Constrói uma lista de DTOs
        /// </summary>
        public List<ClienteDto> BuildList(int quantidade = 5)
        {
            return _faker.Make(quantidade, () => Build()).ToList();
        }

        /// <summary>
        /// Constrói um DTO com dados aleatórios válidos
        /// </summary>
        public ClienteDto BuildValido()
        {
            return new ClienteDtoBuilder()
                .ComNomeEmpresa(_faker.Company.CompanyName())
                .ComPorteEmpresa(_faker.PickRandom<PorteEmpresa>())
                .Build();
        }
    }
}
