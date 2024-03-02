using MediatR;
using POS.Application.Common;
using POS.Application.DTOs.Users;
using POS.Application.Interfaces;

namespace POS.Application.UseCases.Users.Commands
{
	public class LoginUserHandler : IRequestHandler<LoginUserCommand, Response<LoginResponse>>
	{
		private IAuthService _authService;

		public LoginUserHandler(IAuthService authService)
		{
			_authService = authService;
		}

		public async Task<Response<LoginResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
		{
			return await _authService.LoginUser(new LoginRequest { Email = request.Email, Password = request.Password });
		}
	}
}
