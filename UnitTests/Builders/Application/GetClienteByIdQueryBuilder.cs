using Bogus;
using Opea.Application.Queries;

namespace Opea.Tests.Builders.Application
{
    /// <summary>
    /// Builder para criar instâncias de GetClienteByIdQuery para testes usando o padrão Builder
    /// </summary>
    public class GetClienteByIdQueryBuilder
    {
        private readonly Faker _faker;
        private Guid _id;

        public GetClienteByIdQueryBuilder()
        {
            _faker = new Faker("pt_BR");
            _id = Guid.NewGuid();
        }

        /// <summary>
        /// Cria uma nova instância do GetClienteByIdQueryBuilder
        /// </summary>
        public static GetClienteByIdQueryBuilder Novo() => new();

        /// <summary>
        /// Define o ID do cliente
        /// </summary>
        public GetClienteByIdQueryBuilder ComId(Guid id)
        {
            _id = id;
            return this;
        }

        /// <summary>
        /// Define um ID vazio (inválido)
        /// </summary>
        public GetClienteByIdQueryBuilder ComIdVazio()
        {
            _id = Guid.Empty;
            return this;
        }

        /// <summary>
        /// Constrói a instância do GetClienteByIdQuery
        /// </summary>
        public GetClienteByIdQuery Build()
        {
            return new GetClienteByIdQuery(_id);
        }

        /// <summary>
        /// Constrói uma query com dados aleatórios válidos
        /// </summary>
        public GetClienteByIdQuery BuildValido()
        {
            return new GetClienteByIdQueryBuilder()
                .ComId(Guid.NewGuid())
                .Build();
        }
    }
}
