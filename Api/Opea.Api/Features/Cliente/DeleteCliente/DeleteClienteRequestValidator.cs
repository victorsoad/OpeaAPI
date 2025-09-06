using FluentValidation;

namespace Opea.Api.Features
{
    /// <summary>
    /// Validador para a requisição de exclusão de cliente.
    /// </summary>
    public class DeleteClienteRequestValidator : AbstractValidator<DeleteClienteRequest>
    {
        public DeleteClienteRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("O ID do cliente é obrigatório para a exclusão.");
        }
    }
}
