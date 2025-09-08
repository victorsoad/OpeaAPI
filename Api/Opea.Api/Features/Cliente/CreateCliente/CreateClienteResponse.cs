using Opea.Domain.Enums;

namespace Opea.Api.Features
{
    /// <summary>
    /// Representa a resposta após a criação de um novo cliente.
    /// </summary>
    public record CreateClienteResponse(
        Guid Id,
        string NomeEmpresa,
        PorteEmpresa PorteEmpresa
    );
}