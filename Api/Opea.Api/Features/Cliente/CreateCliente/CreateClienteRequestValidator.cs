using FluentValidation;

namespace Opea.Api.Features
{
    /// <summary>
    /// Validador para a classe CreateClienteRequest, utilizando FluentValidation.
    /// </summary>
    public class CreateClienteRequestValidator : AbstractValidator<CreateClienteRequest>
    {
        public CreateClienteRequestValidator()
        {
            // Regra para a propriedade NomeEmpresa.
            RuleFor(request => request.NomeEmpresa)
                .NotEmpty().WithMessage("O nome da empresa é obrigatório.")
                .Length(1, 100).WithMessage("O nome da empresa deve ter entre 1 e 100 caracteres.");

            // Regra para a propriedade PorteEmpresa.
            RuleFor(request => request.PorteEmpresa)
                .IsInEnum().WithMessage("O porte da empresa é inválido.");
        }
    }
}
