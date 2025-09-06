using MediatR;
using Opea.Application.Commands;
using Opea.Application.Services;
using Opea.Domain.Interfaces;

namespace Opea.Application.Handlers
{
    /// <summary>
    /// Handler para o comando de exclusão de cliente.
    /// </summary>
    public class DeleteClienteCommandHandler : IRequestHandler<DeleteClienteCommand, bool>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly ClienteProjectionSyncService _projectionSyncService;

        public DeleteClienteCommandHandler(IClienteRepository clienteRepository, ClienteProjectionSyncService projectionSyncService)
        {
            _clienteRepository = clienteRepository;
            _projectionSyncService = projectionSyncService;
        }

        public async Task<bool> Handle(DeleteClienteCommand request, CancellationToken cancellationToken)
        {
            await _clienteRepository.DeleteAsync(request.Id);
            await _projectionSyncService.SyncAllClientsAsync();
            return true;
        }
    }
}