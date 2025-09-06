using Opea.Domain.Enums;

namespace Opea.Api.Features
{
    /// <summary>
    /// Objeto de requisição para a criação de um novo cliente.
    /// Utilizado para validar a entrada antes de criar o comando de domínio.
    /// </summary>
    public class CreateClienteRequest
    {
        public CreateClienteRequest() { }

        public string NomeEmpresa { get; set; } = string.Empty;

        public PorteEmpresa PorteEmpresa { get; set; }
    }
}
