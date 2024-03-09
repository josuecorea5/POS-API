using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Application.UseCases.Sales.Commands;
using POS.Application.UseCases.Sales.Queries;

namespace POS.App.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SalesController : ControllerBase
	{
		private readonly IMediator _mediador;

		public SalesController(IMediator mediador)
		{
			_mediador = mediador;
		}

		[HttpGet]
		[Authorize(Roles = "BASIC")]
		public async Task<IActionResult> GetAll()
		{
			var response = await _mediador.Send(new GetAllSalesQuery());

			if(response.Success)
				return Ok(response);

			return BadRequest(response.Message);
		}

		[HttpGet("{id}")]
		[Authorize(Roles = "BASIC")]
		public async Task<IActionResult> GetById(int id)
		{
			var response = await _mediador.Send(new GetSaleQuery { Id = id });
			if(response.Success)
				return Ok(response);

			return BadRequest(response.Message);
		}

		[HttpPost]
		[Authorize(Roles = "ADMIN")]
		public async Task<IActionResult> Create(CreateSaleCommand command)
		{
			var response = await _mediador.Send(command);
		
			if(response.Success)
				return Ok(response);

			return BadRequest(response.Message);
		}

		[HttpPut("{id}")]
		[Authorize(Roles = "ADMIN")]
		public async Task<IActionResult> Update(int id, UpdateSaleCommand command)
		{
			var sale = await _mediador.Send(new GetSaleQuery { Id = id });

			if(!sale.Success)
				return NotFound(sale.Message);

			var response = await _mediador.Send(command);

			if(response.Success)
				return Ok(response);

			return BadRequest(response.Message);
		}

		[HttpDelete("{id}")]
		[Authorize(Roles = "ADMIN")]
		public async Task<IActionResult> Delete(int id)
		{
			var sale = await _mediador.Send(new GetSaleQuery { Id = id });

			if(!sale.Success)
			{
				return NotFound(sale.Message);
			}

			var response = await _mediador.Send(new DeleteSaleCommand { Id = id });

			if(response.Success)
				return Ok(response);

			return BadRequest(response.Message);
		}
	}
}
