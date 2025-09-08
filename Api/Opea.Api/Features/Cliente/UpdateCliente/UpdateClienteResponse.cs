using Opea.Domain.Enums;

namespace Opea.Api.Features
{
    /// <summary>
    /// Representa a resposta após a atualização de um cliente.
    /// </summary>
    public record UpdateClienteResponse(
        Guid Id,
        string NomeEmpresa,
        PorteEmpresa PorteEmpresa
    );
}
