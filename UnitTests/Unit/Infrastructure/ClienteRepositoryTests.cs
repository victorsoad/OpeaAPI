using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Opea.Domain.Entities;
using Opea.Domain.Enums;
using Opea.Infrastructure.Data;
using Opea.Infrastructure.Repositories;
using Opea.Tests.Builders.Domain;

namespace Opea.Tests.Unit.Infrastructure
{
    /// <summary>
    /// Testes unitários para o ClienteRepository
    /// </summary>
    public class ClienteRepositoryTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly ClienteRepository _repository;

        public ClienteRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            _repository = new ClienteRepository(_context);
        }

        [Fact]
        public async Task AddAsync_ComClienteValido_DeveAdicionarComSucesso()
        {
            // Arrange
            var cliente = ClienteBuilder.Novo()
                .ComoPequenaEmpresa()
                .Build();

            // Act
            await _repository.AddAsync(cliente);
            var result = cliente;

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(cliente.Id);
            result.NomeEmpresa.Should().Be(cliente.NomeEmpresa);
            result.PorteEmpresa.Should().Be(cliente.PorteEmpresa);

            var clienteNoBanco = await _context.Clientes.FindAsync(cliente.Id);
            clienteNoBanco.Should().NotBeNull();
            clienteNoBanco!.NomeEmpresa.Should().Be(cliente.NomeEmpresa);
        }

        [Fact]
        public async Task GetByIdAsync_ComIdExistente_DeveRetornarCliente()
        {
            // Arrange
            var cliente = ClienteBuilder.Novo()
                .ComoMediaEmpresa()
                .Build();

            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetByIdAsync(cliente.Id);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(cliente.Id);
            result.NomeEmpresa.Should().Be(cliente.NomeEmpresa);
            result.PorteEmpresa.Should().Be(cliente.PorteEmpresa);
        }

        [Fact]
        public async Task GetByIdAsync_ComIdInexistente_DeveRetornarNull()
        {
            // Arrange
            var idInexistente = Guid.NewGuid();

            // Act
            var result = await _repository.GetByIdAsync(idInexistente);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetAllAsync_ComClientesExistentes_DeveRetornarTodosOsClientes()
        {
            // Arrange
            var clientes = ClienteBuilder.Novo().BuildList(3);
            
            foreach (var cliente in clientes)
            {
                await _context.Clientes.AddAsync(cliente);
            }
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
            result.Should().AllBeOfType<Cliente>();
        }

        [Fact]
        public async Task GetAllAsync_ComListaVazia_DeveRetornarListaVazia()
        {
            // Arrange - Banco vazio

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task UpdateAsync_ComClienteExistente_DeveAtualizarComSucesso()
        {
            // Arrange
            var cliente = ClienteBuilder.Novo()
                .ComNomeEmpresa("Nome Original")
                .ComPorteEmpresa(PorteEmpresa.Pequena)
                .Build();

            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();

            // Act
            cliente.Update("Nome Atualizado", PorteEmpresa.Grande);
            await _repository.UpdateAsync(cliente);
            var result = cliente;

            // Assert
            result.Should().NotBeNull();
            result.NomeEmpresa.Should().Be("Nome Atualizado");
            result.PorteEmpresa.Should().Be(PorteEmpresa.Grande);

            var clienteAtualizado = await _context.Clientes.FindAsync(cliente.Id);
            clienteAtualizado!.NomeEmpresa.Should().Be("Nome Atualizado");
            clienteAtualizado.PorteEmpresa.Should().Be(PorteEmpresa.Grande);
        }

        [Fact]
        public async Task DeleteAsync_ComIdExistente_DeveDeletarComSucesso()
        {
            // Arrange
            var cliente = ClienteBuilder.Novo().Build();
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();

            // Act
            await _repository.DeleteAsync(cliente.Id);

            // Assert
            var clienteDeletado = await _context.Clientes.FindAsync(cliente.Id);
            clienteDeletado.Should().BeNull();
        }

        [Fact]
        public async Task DeleteAsync_ComIdInexistente_DeveNaoLancarExcecao()
        {
            // Arrange
            var idInexistente = Guid.NewGuid();

            // Act & Assert
            await _repository.DeleteAsync(idInexistente);
            // Não deve lançar exceção
        }

        [Fact]
        public async Task AddAsync_ComClienteNulo_DeveLancarExcecao()
        {
            // Arrange
            Cliente cliente = null!;

            // Act & Assert
            var action = async () => await _repository.AddAsync(cliente);
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task UpdateAsync_ComClienteNulo_DeveLancarExcecao()
        {
            // Arrange
            Cliente cliente = null!;

            // Act & Assert
            var action = async () => await _repository.UpdateAsync(cliente);
            await action.Should().ThrowAsync<NullReferenceException>();
        }

        [Fact]
        public async Task GetByIdAsync_ComIdVazio_DeveRetornarNull()
        {
            // Arrange
            var idVazio = Guid.Empty;

            // Act
            var result = await _repository.GetByIdAsync(idVazio);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task DeleteAsync_ComIdVazio_DeveNaoLancarExcecao()
        {
            // Arrange
            var idVazio = Guid.Empty;

            // Act & Assert
            await _repository.DeleteAsync(idVazio);
            // Não deve lançar exceção
        }

        [Fact]
        public async Task GetAllAsync_ComClientesDeDiferentesPortes_DeveRetornarTodos()
        {
            // Arrange
            var clientes = new List<Cliente>
            {
                ClienteBuilder.Novo().ComoPequenaEmpresa().Build(),
                ClienteBuilder.Novo().ComoMediaEmpresa().Build(),
                ClienteBuilder.Novo().ComoGrandeEmpresa().Build()
            };

            foreach (var cliente in clientes)
            {
                await _context.Clientes.AddAsync(cliente);
            }
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
            
            var portes = result.Select(c => c.PorteEmpresa).ToList();
            portes.Should().Contain(PorteEmpresa.Pequena);
            portes.Should().Contain(PorteEmpresa.Media);
            portes.Should().Contain(PorteEmpresa.Grande);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
