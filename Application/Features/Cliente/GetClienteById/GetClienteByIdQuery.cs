using MediatR;
using Opea.Application.DTOs;

namespace Opea.Application.Queries
{
    /// <summary>
    /// Query para obter um cliente por ID.
    /// </summary>
    /// <param name="Id">ID do cliente a ser buscado.</param>
    public record GetClienteByIdQuery(Guid Id) : IRequest<ClienteDto>;
}
