using FluentValidation;

namespace Opea.Api.Features
{
    /// <summary>
    /// Validador para a requisição de atualização de cliente.
    /// </summary>
    public class UpdateClienteRequestValidator : AbstractValidator<UpdateClienteRequest>
    {
        public UpdateClienteRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("O ID do cliente é obrigatório.");

            RuleFor(x => x.NomeEmpresa)
                .NotEmpty().WithMessage("O nome da empresa é obrigatório.")
                .MaximumLength(100).WithMessage("O nome da empresa deve ter no máximo 100 caracteres.");

            RuleFor(x => x.PorteEmpresa)
                .IsInEnum().WithMessage("O porte da empresa é inválido.");
        }
    }
}
