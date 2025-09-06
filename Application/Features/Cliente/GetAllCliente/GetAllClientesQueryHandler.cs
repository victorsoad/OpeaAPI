using MediatR;
using Opea.Application.DTOs;
using Opea.Application.Queries;
using Opea.Application.Services;

namespace Opea.Application.Handlers
{
    /// <summary>
    /// Handler para a query de busca de todos os clientes.
    /// </summary>
    public class GetAllClientesQueryHandler : IRequestHandler<GetAllClientesQuery, List<ClienteDto>>
    {
        private readonly ClienteProjectionSyncService _projectionSyncService;

        public GetAllClientesQueryHandler(ClienteProjectionSyncService projectionSyncService)
        {
            _projectionSyncService = projectionSyncService;
        }

        public async Task<List<ClienteDto>> Handle(GetAllClientesQuery request, CancellationToken cancellationToken)
        {
            // A consulta lê da projeção, que é a parte de leitura no padrão CQRS.
            var clientes = await _projectionSyncService.GetProjectionAsync();
            return clientes?.ToList() ?? new List<ClienteDto>();
        }
    }
}
