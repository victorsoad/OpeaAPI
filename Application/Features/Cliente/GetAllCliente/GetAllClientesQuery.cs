using MediatR;
using Opea.Application.DTOs;

namespace Opea.Application.Queries
{
    /// <summary>
    /// Query para listar todos os clientes.
    /// Retorna uma lista de objetos ClienteDto.
    /// </summary>
    public record GetAllClientesQuery : IRequest<List<ClienteDto>>;
}
