using MediatR;
using POS.Application.Common;
using POS.Application.DTOs.Users;

namespace POS.Application.UseCases.Users.Commands
{
	public sealed record LoginUserCommand : IRequest<Response<LoginResponse>>
	{
		public string Email { get; set; }
		public string Password { get; set; }
	}
}
