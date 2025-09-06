using MediatR;

namespace Opea.Application.Commands
{
    /// <summary>
    /// Comando para a exclusão de um cliente existente.
    /// Implementa a interface IRequest do MediatR com um retorno booleano.
    /// </summary>
    /// <param name="Id">ID do cliente a ser excluído.</param>
    public record DeleteClienteCommand(Guid Id) : IRequest<bool>;
}
