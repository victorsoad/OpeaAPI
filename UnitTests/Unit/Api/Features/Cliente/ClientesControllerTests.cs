using Bogus;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Opea.Api.Features;
using Opea.Application.Commands;
using Opea.Domain.Enums;

namespace Opea.Tests.Features
{
    public class ClientesControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly ClientesController _controller;
        private readonly Faker _faker;

        public ClientesControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new ClientesController(_mediatorMock.Object);
            _faker = new Faker("pt_BR");
        }

        [Fact]
        public async Task PostCliente_DeveRetornarCreated_QuandoRequisicaoValida()
        {
            // Arrange
            var request = new CreateClienteRequest
            {
                NomeEmpresa = _faker.Company.CompanyName(),
                PorteEmpresa = PorteEmpresa.Pequena
            };

            var clienteCriado = new Domain.Entities.Cliente(request.NomeEmpresa, request.PorteEmpresa);           

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<CreateClienteCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(clienteCriado);

            // Act
            var result = await _controller.PostCliente(request);

            // Assert
            result.Should().BeOfType<ActionResult<CreateClienteResponse>>();
            var createdResult = result.Result as CreatedAtActionResult;
            createdResult.Should().NotBeNull();
            createdResult!.StatusCode.Should().Be(StatusCodes.Status201Created);
            createdResult!.ActionName.Should().Be(nameof(_controller.GetCliente));
            var response = createdResult.Value as CreateClienteResponse;
            response.Should().NotBeNull();
            response!.NomeEmpresa.Should().Be(request.NomeEmpresa);
            response.PorteEmpresa.Should().Be(request.PorteEmpresa);
        }
        
        [Fact]
        public async Task PutCliente_DeveRetornarOk_QuandoRequisicaoValida()
        {
            // Arrange
            var id = Guid.NewGuid();
            var request = new UpdateClienteRequest
            {
                NomeEmpresa = _faker.Company.CompanyName(),
                PorteEmpresa = PorteEmpresa.Grande
            };

            var clienteAtualizado = new Opea.Domain.Entities.Cliente(request.NomeEmpresa, request.PorteEmpresa);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<UpdateClienteCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(clienteAtualizado);

            // Act
            var result = await _controller.PutCliente(id, request);

            // Assert
            result.Should().BeOfType<ActionResult<UpdateClienteResponse>>();
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(StatusCodes.Status200OK);
            var response = okResult.Value as UpdateClienteResponse;
            response.Should().NotBeNull();
            response!.NomeEmpresa.Should().Be(request.NomeEmpresa);
        }
        /*
        [Fact]
        public async Task DeleteCliente_DeveRetornarNoContent_QuandoClienteExiste()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<DeleteClienteCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteCliente(id);

            // Assert
            result.Should().BeOfType<NoContentResult>();
            var noContentResult = result as NoContentResult;
            noContentResult.Should().NotBeNull();
            noContentResult!.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        [Fact]
        public async Task GetAllClientes_DeveRetornarOkComListaDeClientes()
        {
            // Arrange
            var clientes = new Faker<GetClienteResponse>("pt_BR")
                .CustomInstantiator(f => new GetClienteResponse(
                    Guid.NewGuid(),
                    f.Company.CompanyName(),
                    f.PickRandom<PorteEmpresa>().ToString()
                ))
                .Generate(3);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetAllClientesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(clientes);

            // Act
            var result = await _controller.GetAllClientes();

            // Assert
            result.Should().BeOfType<ActionResult<IEnumerable<GetClienteResponse>>>();
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(StatusCodes.Status200OK);
            var response = okResult.Value as IEnumerable<GetClienteResponse>;
            response.Should().NotBeNull();
            response!.Should().HaveCount(3);
        }

        [Fact]
        public async Task GetCliente_DeveRetornarOk_QuandoClienteExiste()
        {
            // Arrange
            var id = Guid.NewGuid();
            var cliente = new GetClienteResponse(
                id,
                _faker.Company.CompanyName(),
                _faker.PickRandom<PorteEmpresa>().ToString()
            );
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetClienteByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(cliente);

            // Act
            var result = await _controller.GetCliente(id);

            // Assert
            result.Should().BeOfType<ActionResult<GetClienteResponse>>();
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(StatusCodes.Status200OK);
            var response = okResult.Value as GetClienteResponse;
            response.Should().NotBeNull();
            response!.Id.Should().Be(id);
        }

        [Fact]
        public async Task GetCliente_DeveRetornarNotFound_QuandoClienteNaoExiste()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetClienteByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((GetClienteResponse)null);

            // Act
            var result = await _controller.GetCliente(id);

            // Assert
            result.Should().BeOfType<ActionResult<GetClienteResponse>>();
            result.Result.Should().BeOfType<NotFoundResult>();
        }*/
    }
}
