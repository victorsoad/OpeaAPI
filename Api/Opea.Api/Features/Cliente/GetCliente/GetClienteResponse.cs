using System;

namespace Opea.Api.Features
{
    /// <summary>
    /// Representa a resposta de um cliente para requisições GET.
    /// </summary>
    public record GetClienteResponse(
        Guid Id,
        string NomeDaEmpresa,
        string PorteDaEmpresa
    );
}
