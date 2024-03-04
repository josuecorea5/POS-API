using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Application.UseCases.Clients.Commands;
using POS.Application.UseCases.Clients.Queries.Clients;

namespace POS.App.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ClientsController : ControllerBase
	{
		private readonly IMediator _mediator;

		public ClientsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		[Authorize(Roles = "BASIC")]
		public async Task<IActionResult> GetAll()
		{
			var response = await _mediator.Send(new GetAllClientsQuery());

			if(response.Success)
				return Ok(response);

			return BadRequest(response.Message);
		}

		[HttpGet("{id}")]
		[Authorize(Roles = "BASIC")]
		public async Task<IActionResult> GetById(int id)
		{
			var response = await _mediator.Send(new GetClientQuery { Id = id });
			if(response.Success) return Ok(response);

			return BadRequest(response.Message);
		}

		[HttpPost, Authorize(Roles = "ADMIN")]
		public async Task<IActionResult> Create(CreateClientCommand command)
		{
			if (command == null) return BadRequest();

			var response = await _mediator.Send(command);

			if (response.Success)
				return Ok(response);

			return BadRequest(response.Message);
		}

		[HttpPut("{id}")]
		[Authorize(Roles = "ADMIN")]
		public async Task<IActionResult> Update(int id, UpdateClientCommand command)
		{
			var client = await _mediator.Send(new GetClientQuery() { Id= id });

			if(!client.Success)
				return NotFound(client.Message);

			var updateClient = await _mediator.Send(command);

			if(updateClient.Success)
				return Ok(updateClient);

			return BadRequest(updateClient.Message);
		}

		[HttpDelete("{id}")]
		[Authorize(Roles = "ADMIN")]
		public async Task<IActionResult> Delete(int id)
		{
			var client = await _mediator.Send(new GetClientQuery { Id= id });
			if(!client.Success)
				return NotFound(client.Message);

			var deleteClient = await _mediator.Send(new DeleteClientCommand { Id = id});

			if(deleteClient.Success)
				return Ok(deleteClient);

			return BadRequest(deleteClient.Message);
		}
	}
}
