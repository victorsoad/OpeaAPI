using MediatR;
using Microsoft.AspNetCore.Mvc;
using Opea.Application.Commands;
using Opea.Application.Queries;
using System.Net.Mime;

namespace Opea.Api.Features
{
    [ApiController]
    [Route("api/[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class ClientesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Cria um novo cliente.
        /// </summary>
        /// <param name="request">Dados para criação do cliente.</param>
        /// <returns>Dados do cliente criado.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateClienteResponse>> PostCliente(CreateClienteRequest request)
        {
            var command = new CreateClienteCommand(request.NomeEmpresa, request.PorteEmpresa);
            var cliente = await _mediator.Send(command);

            var response = new CreateClienteResponse(cliente.Id, cliente.NomeEmpresa, cliente.PorteEmpresa.ToString());
            return CreatedAtAction(nameof(GetCliente), new { id = response.Id }, response);
        }

        /// <summary>
        /// Atualiza um cliente existente.
        /// </summary>
        /// <param name="id">ID do cliente a ser atualizado.</param>
        /// <param name="request">Novos dados para o cliente.</param>
        /// <returns>Dados do cliente atualizado.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UpdateClienteResponse>> PutCliente(Guid id, UpdateClienteRequest request)
        {
            var command = new UpdateClienteCommand(id, request.NomeEmpresa, request.PorteEmpresa);
            var cliente = await _mediator.Send(command);

            var response = new UpdateClienteResponse(cliente.Id, cliente.NomeEmpresa, cliente.PorteEmpresa.ToString());
            return Ok(response);
        }

        /// <summary>
        /// Remove um cliente pelo seu ID.
        /// </summary>
        /// <param name="id">ID do cliente a ser removido.</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCliente(Guid id)
        {
            var command = new DeleteClienteCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Retorna a lista de todos os clientes.
        /// </summary>
        /// <returns>Lista de clientes.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GetClienteResponse>>> GetAllClientes()
        {
            var query = new GetAllClientesQuery();
            var clientes = await _mediator.Send(query);
            return Ok(clientes);
        }

        /// <summary>
        /// Retorna um cliente pelo seu ID.
        /// </summary>
        /// <param name="id">ID do cliente.</param>
        /// <returns>Dados do cliente.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetClienteResponse>> GetCliente(Guid id)
        {
            var query = new GetClienteByIdQuery(id);
            var cliente = await _mediator.Send(query);

            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }
    }
}
