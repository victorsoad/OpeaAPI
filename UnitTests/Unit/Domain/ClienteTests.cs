using FluentAssertions;
using Opea.Domain.Entities;
using Opea.Domain.Enums;
using Opea.Tests.Builders.Domain;

namespace Opea.Tests.Unit.Domain
{
    /// <summary>
    /// Testes unitários para a entidade Cliente
    /// </summary>
    public class ClienteTests
    {
        [Fact]
        public void Construtor_ComParametrosValidos_DeveCriarClienteComSucesso()
        {
            // Arrange
            var nomeEmpresa = "Empresa Teste LTDA";
            var porteEmpresa = PorteEmpresa.Pequena;

            // Act
            var cliente = new Cliente(nomeEmpresa, porteEmpresa);

            // Assert
            cliente.Should().NotBeNull();
            cliente.Id.Should().NotBeEmpty();
            cliente.NomeEmpresa.Should().Be(nomeEmpresa);
            cliente.PorteEmpresa.Should().Be(porteEmpresa);
        }

        [Fact]
        public void Construtor_ComParametrosValidos_DeveGerarIdUnico()
        {
            // Arrange & Act
            var cliente1 = ClienteBuilder.Novo().Build();
            var cliente2 = ClienteBuilder.Novo().Build();

            // Assert
            cliente1.Id.Should().NotBe(cliente2.Id);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Construtor_ComNomeEmpresaInvalido_DeveLancarExcecao(string nomeEmpresaInvalido)
        {
            // Arrange & Act & Assert
            var action = () => new Cliente(nomeEmpresaInvalido!, PorteEmpresa.Pequena);
            action.Should().Throw<ArgumentException>()
                .WithMessage("Nome da empresa não pode ser nulo ou vazio*");
        }

        [Fact]
        public void Update_ComParametrosValidos_DeveAtualizarClienteComSucesso()
        {
            // Arrange
            var cliente = ClienteBuilder.Novo()
                .ComNomeEmpresa("Empresa Original")
                .ComPorteEmpresa(PorteEmpresa.Pequena)
                .Build();

            var novoNome = "Empresa Atualizada S.A.";
            var novoPorte = PorteEmpresa.Media;

            // Act
            cliente.Update(novoNome, novoPorte);

            // Assert
            cliente.NomeEmpresa.Should().Be(novoNome);
            cliente.PorteEmpresa.Should().Be(novoPorte);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Update_ComNomeEmpresaInvalido_DeveLancarExcecao(string nomeEmpresaInvalido)
        {
            // Arrange
            var cliente = ClienteBuilder.Novo().Build();

            // Act & Assert
            var action = () => cliente.Update(nomeEmpresaInvalido!, PorteEmpresa.Pequena);
            action.Should().Throw<ArgumentException>()
                .WithMessage("Nome da empresa não pode ser nulo ou vazio*");
        }

        [Fact]
        public void Update_ComPorteEmpresaValido_DeveAtualizarComSucesso()
        {
            // Arrange
            var cliente = ClienteBuilder.Novo()
                .ComPorteEmpresa(PorteEmpresa.Pequena)
                .Build();

            // Act
            cliente.Update("Nova Empresa", PorteEmpresa.Grande);

            // Assert
            cliente.PorteEmpresa.Should().Be(PorteEmpresa.Grande);
        }

        [Fact]
        public void Construtor_ComTodosOsPortesEmpresa_DeveCriarComSucesso()
        {
            // Arrange & Act
            var clientePequena = ClienteBuilder.Novo().ComoPequenaEmpresa().Build();
            var clienteMedia = ClienteBuilder.Novo().ComoMediaEmpresa().Build();
            var clienteGrande = ClienteBuilder.Novo().ComoGrandeEmpresa().Build();

            // Assert
            clientePequena.PorteEmpresa.Should().Be(PorteEmpresa.Pequena);
            clienteMedia.PorteEmpresa.Should().Be(PorteEmpresa.Media);
            clienteGrande.PorteEmpresa.Should().Be(PorteEmpresa.Grande);
        }

        [Fact]
        public void Construtor_ComBogus_DeveCriarClienteValido()
        {
            // Arrange & Act
            var cliente = ClienteBuilder.Novo().BuildValido();

            // Assert
            cliente.Should().NotBeNull();
            cliente.Id.Should().NotBeEmpty();
            cliente.NomeEmpresa.Should().NotBeNullOrEmpty();
            cliente.PorteEmpresa.Should().BeOneOf(PorteEmpresa.Pequena, PorteEmpresa.Media, PorteEmpresa.Grande);
        }
    }
}
