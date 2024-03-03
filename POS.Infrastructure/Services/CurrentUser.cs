﻿using Microsoft.AspNetCore.Http;
using POS.Application.Interfaces;
using System.Security.Claims;

namespace POS.Infrastructure.Services
{
	public class CurrentUser : ICurrentUser
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public CurrentUser(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public string GetUserId()
		{
			return _httpContextAccessor.HttpContext?.User?.FindFirstValue("sub");
		}
	}
}
