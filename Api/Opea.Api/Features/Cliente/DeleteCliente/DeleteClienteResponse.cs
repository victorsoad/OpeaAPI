namespace Opea.Api.Features
{
    /// <summary>
    /// Objeto de resposta para a exclusão de um cliente.
    /// Retorna uma mensagem de confirmação de sucesso.
    /// </summary>
    public class DeleteClienteResponse
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = "Cliente excluído com sucesso.";
    }
}
