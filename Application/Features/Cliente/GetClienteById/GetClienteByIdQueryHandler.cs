using MediatR;
using Opea.Application.DTOs;
using Opea.Application.Queries;
using Opea.Application.Services;

namespace Opea.Application.Handlers
{
    /// <summary>
    /// Handler para a query de busca de cliente por ID.
    /// </summary>
    public class GetClienteByIdQueryHandler : IRequestHandler<GetClienteByIdQuery, ClienteDto>
    {
        private readonly ClienteProjectionSyncService _projectionSyncService;

        public GetClienteByIdQueryHandler(ClienteProjectionSyncService projectionSyncService)
        {
            _projectionSyncService = projectionSyncService;
        }

        public async Task<ClienteDto> Handle(GetClienteByIdQuery request, CancellationToken cancellationToken)
        {
            var clientes = await _projectionSyncService.GetProjectionAsync();
            var cliente = clientes.FirstOrDefault(c => c.Id == request.Id);
            return cliente;
        }
    }
}
