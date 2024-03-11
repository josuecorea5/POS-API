using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Application.UseCases.SchedulePayments.Commands;
using POS.Application.UseCases.SchedulePayments.Queries;

namespace POS.App.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SchedulePaymentsController : ControllerBase
	{
		private readonly IMediator _mediator;

		public SchedulePaymentsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		[Authorize(Roles = "BASIC")]
		public async Task<IActionResult> GetAll()
		{
			var response = await _mediator.Send(new GetAllSchedulePaymentsQuery());

			if(response.Success)
				return Ok(response);

			return BadRequest(response.Message);
		}

		[HttpGet("{id}")]
		[Authorize(Roles = "BASIC")]
		public async Task<IActionResult> GetById(int id)
		{
			var response = await _mediator.Send(new GetSchedulePaymentQuery { Id = id });

			if(response.Success) return Ok(response);

			return BadRequest(response.Message);
		}

		[HttpPost]
		[Authorize(Roles = "ADMIN")]
		public async Task<IActionResult> Create(CreateSchedulePaymentCommand command)
		{
			var response = await _mediator.Send(command);

			if(response.Success) 
				return Ok(response);

			return BadRequest(response.Message);
		}

		[HttpPut("{id}")]
		[Authorize(Roles = "ADMIN")]
		public async Task<IActionResult> Update(int id, UpdateSchedulePaymentCommand command)
		{
			var schedulePayment = await _mediator.Send(new GetSchedulePaymentQuery { Id = id });

			if (!schedulePayment.Success)
				return NotFound(schedulePayment.Message);

			var response = await _mediator.Send(command);

			if (response.Success) 
				return Ok(response);

			return BadRequest(response.Message);
		}

		[HttpPut("status/{id}")]
		[Authorize(Roles = "ADMIN")]
		public async Task<IActionResult> UpdateStatus(int id, UpdateSchedulePaymentStatusCommand command)
		{
			var schedulePayment = await _mediator.Send(new GetSchedulePaymentQuery { Id=id });

			if(!schedulePayment.Success)
				return NotFound(schedulePayment.Message);

			var response = await _mediator.Send(command);

			if(response.Success)
				return Ok(response);

			return BadRequest(response.Message);
		}
	}
}
