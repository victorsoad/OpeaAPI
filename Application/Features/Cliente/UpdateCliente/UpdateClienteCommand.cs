using MediatR;
using Opea.Domain.Enums;
using Opea.Domain.Entities;

namespace Opea.Application.Commands
{
    /// <summary>
    /// Comando para a atualização de um cliente.
    /// Implementa a interface IRequest do MediatR para retornar a entidade Cliente.
    /// </summary>
    /// <param name="Id">ID do cliente a ser atualizado.</param>
    /// <param name="NomeEmpresa">Novo nome da empresa.</param>
    /// <param name="PorteEmpresa">Novo porte da empresa.</param>
    public record UpdateClienteCommand(Guid Id, string NomeEmpresa, PorteEmpresa PorteEmpresa) : IRequest<Cliente>;
}
