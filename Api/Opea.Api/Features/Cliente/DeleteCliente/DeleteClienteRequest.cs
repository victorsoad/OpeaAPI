namespace Opea.Api.Features
{
    /// <summary>
    /// Objeto de requisição para a exclusão de um cliente.
    /// Contém o ID do cliente a ser removido.
    /// </summary>
    public class DeleteClienteRequest
    {
        public Guid Id { get; set; }
    }
}
