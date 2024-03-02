using MediatR;
using POS.Application.Common;
using POS.Application.DTOs.Users;
using POS.Application.Interfaces;

namespace POS.Application.UseCases.Users.Commands
{
	public class CreateUserHandler : IRequestHandler<CreateUserCommand, Response<RegisterResponse>>
	{
		private readonly IAuthService _authService;

		public CreateUserHandler(IAuthService authService)
		{
			_authService = authService;
		}

		public async Task<Response<RegisterResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
		{
			return await _authService.RegisterUser(new RegisterRequest { 
				Email = request.Email, UserName = request.UserName, Password = request.Password
			});
		}
	}
}
