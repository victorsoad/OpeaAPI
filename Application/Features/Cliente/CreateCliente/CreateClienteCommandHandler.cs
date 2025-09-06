using MediatR;
using Opea.Application.Commands;
using Opea.Application.Services;
using Opea.Domain.Entities;
using Opea.Domain.Interfaces;

namespace Opea.Application.Handlers
{
    /// <summary>
    /// Handler para o comando de criação de cliente.
    /// </summary>
    public class CreateClienteCommandHandler : IRequestHandler<CreateClienteCommand, Cliente>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly ClienteProjectionSyncService _projectionSyncService;

        public CreateClienteCommandHandler(IClienteRepository clienteRepository, ClienteProjectionSyncService projectionSyncService)
        {
            _clienteRepository = clienteRepository;
            _projectionSyncService = projectionSyncService;
        }

        public async Task<Cliente> Handle(CreateClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = new Cliente(request.NomeEmpresa, request.PorteEmpresa);
            await _clienteRepository.AddAsync(cliente);

            await _projectionSyncService.SyncAllClientsAsync();
            return cliente;
        }
    }
}