using Opea.Domain.Entities;

namespace Opea.Domain.Interfaces
{
    // A interface do repositório, que define o contrato para
    // as operações de acesso a dados. A implementação estará
    // na camada de Infraestrutura.
    public interface IClienteRepository
    {
        Task<Cliente> GetByIdAsync(Guid id);
        Task<IEnumerable<Cliente>> GetAllAsync();
        Task AddAsync(Cliente cliente);
        Task UpdateAsync(Cliente cliente);
        Task DeleteAsync(Guid id);
    }
}
