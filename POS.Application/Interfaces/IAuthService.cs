using POS.Application.Common;
using POS.Application.DTOs.Users;

namespace POS.Application.Interfaces
{
	public interface IAuthService
	{
		Task<Response<RegisterResponse>> RegisterUser(RegisterRequest request);
		Task<Response<LoginResponse>> LoginUser(LoginRequest request);
	}
}
