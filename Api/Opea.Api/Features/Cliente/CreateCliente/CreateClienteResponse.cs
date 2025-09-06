namespace Opea.Api.Features
{
    /// <summary>
    /// Representa a resposta após a criação de um novo cliente.
    /// </summary>
    public record CreateClienteResponse(
        Guid Id,
        string NomeDaEmpresa,
        string PorteDaEmpresa
    );
}
