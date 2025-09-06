using Opea.Domain.Enums;

namespace Opea.Api.Features
{
    /// <summary>
    /// Objeto de requisição para a atualização de um cliente existente.
    /// A validação é realizada pela classe UpdateClienteRequestValidator (FluentValidation).
    /// </summary>
    public class UpdateClienteRequest
    {
        public Guid Id { get; set; }
        public string NomeEmpresa { get; set; } = string.Empty;
        public PorteEmpresa PorteEmpresa { get; set; }
    }
}
