using Opea.Domain.Enums;
using MediatR;
using Opea.Domain.Entities;

namespace Opea.Application.Commands
{
    /// <summary>
    /// Comando para a criação de um novo cliente.
    /// Implementa a interface IRequest do MediatR para um retorno do tipo Cliente.
    /// </summary>
    /// <param name="NomeEmpresa">Nome da empresa a ser criada.</param>
    /// <param name="PorteEmpresa">Porte da empresa a ser criada.</param>
    public record CreateClienteCommand(string NomeEmpresa, PorteEmpresa PorteEmpresa) : IRequest<Cliente>;
}
