using MediatR;
using Opea.Application.Commands;
using Opea.Application.Services;
using Opea.Domain.Entities;
using Opea.Domain.Interfaces;

namespace Opea.Application.Handlers
{
    /// <summary>
    /// Handler para o comando de atualização de cliente.
    /// </summary>
    public class UpdateClienteCommandHandler : IRequestHandler<UpdateClienteCommand, Cliente>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly ClienteProjectionSyncService _projectionSyncService;

        public UpdateClienteCommandHandler(IClienteRepository clienteRepository, ClienteProjectionSyncService projectionSyncService)
        {
            _clienteRepository = clienteRepository;
            _projectionSyncService = projectionSyncService;
        }

        public async Task<Cliente> Handle(UpdateClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = await _clienteRepository.GetByIdAsync(request.Id);

            if (cliente == null)
                return null;

            cliente.Update(request.NomeEmpresa, request.PorteEmpresa);

            await _clienteRepository.UpdateAsync(cliente);
            await _projectionSyncService.SyncAllClientsAsync();
            return cliente;
        }
    }
}
