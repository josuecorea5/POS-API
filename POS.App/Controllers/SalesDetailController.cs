using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Application.UseCases.SaleDetails.Commands;
using POS.Application.UseCases.SaleDetails.Queries;

namespace POS.App.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SalesDetailController : ControllerBase
	{
		private readonly IMediator _mediator;

		public SalesDetailController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPut("{id}")]
		[Authorize(Roles = "ADMIN")]
		public async Task<IActionResult> Update(int id, UpdateSaleDetailCommand command)
		{
			var saleDetail = await _mediator.Send(new GetSaleDetailQuery { Id = id });

			if(!saleDetail.Success)
				return NotFound(saleDetail.Message);

			var response = await _mediator.Send(command);

			if(response.Success)
				return Ok(response);

			return BadRequest(response.Message);
		}

		[HttpDelete("{id}")]
		[Authorize(Roles = "ADMIN")]
		public async Task<IActionResult> Delete(int id)
		{
			var saleDetail = await _mediator.Send(new GetSaleDetailQuery { Id = id });

			if (!saleDetail.Success)
				return NotFound(saleDetail.Message);

			var response = await _mediator.Send(new DeleteSaleDetailCommand { Id = id });

			if (response.Success)
				return Ok(response);

			return BadRequest(response.Message);
		}
	}
}
