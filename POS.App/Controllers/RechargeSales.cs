using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Application.UseCases.RechargeSales.Command;
using POS.Application.UseCases.RechargeSales.Queries;

namespace POS.App.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RechargeSales : ControllerBase
	{
		private readonly IMediator _mediador;

		public RechargeSales(IMediator mediador)
		{
			_mediador = mediador;
		}

		[HttpGet]
		[Authorize(Roles = "BASIC")]
		public async Task<IActionResult> GetAll()
		{
			var response = await _mediador.Send(new GetAllRechargeSalesQuery());
			if(response.Success)
				return Ok(response);

			return BadRequest(response.Message);
		}

		[HttpGet("{id}")]
		[Authorize(Roles = "BASIC")]
		public async Task<IActionResult> GetById(int id)
		{
			var response = await _mediador.Send(new GetRechargeSaleQuery { Id = id});
			if (response.Success)
				return Ok(response);

			return BadRequest(response.Message);
		}

		[HttpPost]
		[Authorize(Roles = "ADMIN")]
		public async Task<IActionResult> Create(CreateRechargeSaleCommand command)
		{
			var response = await _mediador.Send(command);

			if(response.Success)
				return Ok(response);

			return BadRequest(response.Message);
		}

		[HttpPost("{id}/new")]
		[Authorize(Roles = "ADMIN")]
		public async Task<IActionResult> CreateNewRecharge(int id, CreateNewRechargeSaleCommand command)
		{
			var rechargeSale = await _mediador.Send(new GetRechargeSaleQuery { Id = id });

			if(!rechargeSale.Success)
				return NotFound(rechargeSale.Message);

			var response = await _mediador.Send(command);

			if(response.Success)
				return Ok(response);

			return BadRequest(response.Message);
		}

		[HttpPut("{id}")]
		[Authorize(Roles = "ADMIN")]
		public async Task<IActionResult> Update(int id, UpdateRechargeSaleCommand command)
		{
			var rechargeSale = await _mediador.Send(new GetRechargeSaleQuery { Id = id });

			if(!rechargeSale.Success)
				return NotFound(rechargeSale.Message);

			var response = await _mediador.Send(command);

			if(response.Success)
				return Ok(response);

			return BadRequest(response.Message);
		}

		[HttpPut("status/{id}")]
		[Authorize(Roles = "ADMIN")]
		public async Task<IActionResult> UpdateStatus(int id, UpdateRechargeSaleStatusCommand command)
		{
			var rechargeSale = await _mediador.Send(new GetRechargeSaleQuery { Id = id });

			if (!rechargeSale.Success)
				return NotFound(rechargeSale.Message);

			var response = await _mediador.Send(command);

			if(response.Success)
				return Ok(response);

			return BadRequest(response.Message);
		}

		[HttpDelete("{id}")]
		[Authorize(Roles = "ADMIN")]
		public async Task<IActionResult> Delete(int id)
		{
			var rechargeSale = await _mediador.Send(new GetRechargeSaleQuery { Id = id});

			if (!rechargeSale.Success)
				return NotFound(rechargeSale.Message);

			var response = await _mediador.Send(new DeleteRechargeSaleCommand { Id = id });
			
			if(response.Success)
				return Ok(response);

			return BadRequest(response.Message);
		}
	}
}
