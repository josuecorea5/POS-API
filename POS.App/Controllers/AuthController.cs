using MediatR;
using Microsoft.AspNetCore.Mvc;
using POS.Application.UseCases.Users.Commands;

namespace POS.App.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IMediator _mediator;

		public AuthController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost("register")]
		public async Task<IActionResult> RegisterUser(CreateUserCommand command)
		{
			var response = await _mediator.Send(command);

			if(response.Success)
				return Ok(response);

			return BadRequest(response);
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginUserCommand command)
		{
			var response = await _mediator.Send(command);

			if(response.Success)
				return Ok(response);

			return BadRequest(response);
		}
	}
}
