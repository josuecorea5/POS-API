using Microsoft.AspNetCore.Http;
using POS.Application.Interfaces;
using System.Security.Claims;

namespace POS.Infrastructure.Services
{
	public class CurrentUserService : ICurrentUserService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public CurrentUserService(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public string GetUserId()
		{
			return _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
		}
	}
}
