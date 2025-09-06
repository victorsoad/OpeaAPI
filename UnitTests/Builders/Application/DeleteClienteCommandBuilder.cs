using Bogus;
using Opea.Application.Commands;

namespace Opea.Tests.Builders.Application
{
    /// <summary>
    /// Builder para criar instâncias de DeleteClienteCommand para testes usando o padrão Builder
    /// </summary>
    public class DeleteClienteCommandBuilder
    {
        private readonly Faker _faker;
        private Guid _id;

        public DeleteClienteCommandBuilder()
        {
            _faker = new Faker("pt_BR");
            _id = Guid.NewGuid();
        }

        /// <summary>
        /// Cria uma nova instância do DeleteClienteCommandBuilder
        /// </summary>
        public static DeleteClienteCommandBuilder Novo() => new();

        /// <summary>
        /// Define o ID do cliente
        /// </summary>
        public DeleteClienteCommandBuilder ComId(Guid id)
        {
            _id = id;
            return this;
        }

        /// <summary>
        /// Define um ID vazio (inválido)
        /// </summary>
        public DeleteClienteCommandBuilder ComIdVazio()
        {
            _id = Guid.Empty;
            return this;
        }

        /// <summary>
        /// Constrói a instância do DeleteClienteCommand
        /// </summary>
        public DeleteClienteCommand Build()
        {
            return new DeleteClienteCommand(_id);
        }

        /// <summary>
        /// Constrói um comando com dados aleatórios válidos
        /// </summary>
        public DeleteClienteCommand BuildValido()
        {
            return new DeleteClienteCommandBuilder()
                .ComId(Guid.NewGuid())
                .Build();
        }
    }
}
