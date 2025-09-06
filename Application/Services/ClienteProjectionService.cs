using Opea.Application.DTOs;
using Opea.Domain.Interfaces;
using System.Text.Json;

namespace Opea.Application.Services
{
    // Este serviço é responsável por ler os dados do repositório principal
    // e salvá-los no arquivo de projeção (simulando um banco de dados NoSQL).
    // Isso centraliza a lógica de sincronização em um único local.
    public class ClienteProjectionSyncService
    {
        private readonly IClienteRepository _repository;
        private const string _projectionFilePath = "clientes-projection.json";

        public ClienteProjectionSyncService(IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task SyncAllClientsAsync()
        {
            var clientes = await _repository.GetAllAsync();
            var dtos = clientes.Select(c => new ClienteDto
            {
                Id = c.Id,
                NomeEmpresa = c.NomeEmpresa,
                PorteEmpresa = c.PorteEmpresa
            }).ToList();

            var json = JsonSerializer.Serialize(dtos, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_projectionFilePath, json);
        }

        public async Task<IEnumerable<ClienteDto>> GetProjectionAsync()
        {
            if (!File.Exists(_projectionFilePath))
            {
                return Enumerable.Empty<ClienteDto>();
            }

            var json = await File.ReadAllTextAsync(_projectionFilePath);
            return JsonSerializer.Deserialize<List<ClienteDto>>(json);
        }
    }
}
