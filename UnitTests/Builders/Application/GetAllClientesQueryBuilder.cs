using Opea.Application.Queries;

namespace Opea.Tests.Builders.Application
{
    /// <summary>
    /// Builder para criar instâncias de GetAllClientesQuery para testes usando o padrão Builder
    /// </summary>
    public class GetAllClientesQueryBuilder
    {
        /// <summary>
        /// Cria uma nova instância do GetAllClientesQueryBuilder
        /// </summary>
        public static GetAllClientesQueryBuilder Novo() => new();

        /// <summary>
        /// Constrói a instância do GetAllClientesQuery
        /// </summary>
        public GetAllClientesQuery Build()
        {
            return new GetAllClientesQuery();
        }

        /// <summary>
        /// Constrói uma query válida
        /// </summary>
        public GetAllClientesQuery BuildValido()
        {
            return new GetAllClientesQuery();
        }
    }
}
