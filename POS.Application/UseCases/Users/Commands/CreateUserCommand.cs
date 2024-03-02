using MediatR;
using POS.Application.Common;
using POS.Application.DTOs.Users;

namespace POS.Application.UseCases.Users.Commands
{
	public sealed record CreateUserCommand : IRequest<Response<RegisterResponse>>
	{
		public string UserName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
	}
}
