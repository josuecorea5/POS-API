using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using POS.App.Helpers;
using POS.Application.Common;
using POS.Application.DTOs.Users;
using POS.Application.Enums;
using POS.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace POS.Infrastructure.Services
{
	public class AuthService : IAuthService
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly AppSettings _appSettings;

		public AuthService(UserManager<IdentityUser> userManager, IOptions<AppSettings> appSettings)
		{
			_userManager = userManager;
			_appSettings = appSettings.Value;
		}

		public async Task<Response<LoginResponse>> LoginUser(LoginRequest request)
		{
			var response = new Response<LoginResponse>();
			var identityUser = await _userManager.FindByEmailAsync(request.Email);
			if(identityUser is null)
			{
				response.Message = "User not found";
				return response;
			}

			var roleClaims = await _userManager.GetRolesAsync(identityUser);

			if (await _userManager.CheckPasswordAsync(identityUser, request.Password))
			{
				var token = GenerateToken(identityUser, roleClaims);
				response.Success = true;
				response.Message = "Login successfully";
				response.Data = new LoginResponse { Email = identityUser.Email, UserName = identityUser.UserName, Token = token };

				return response;
			}

			response.Message = "Incorrect credentials";

			return response;
		}

		public async Task<Response<RegisterResponse>> RegisterUser(RegisterRequest request)
		{
			var response = new Response<RegisterResponse>();
			var identityUser = new IdentityUser { UserName = request.UserName, Email = request.Email };

			var user = await _userManager.CreateAsync(identityUser, request.Password);

			if(user.Succeeded)
			{
				await _userManager.AddToRoleAsync(identityUser, Roles.BASIC.ToString());
				response.Success = true;
				response.Message = "User registered successfully";
				response.Data = new RegisterResponse { Id = identityUser.Id, Email = identityUser.Email, UserName = identityUser.UserName };

				return response;
			}

			response.Message = "Something failed to create user";
			response.Errors = user.Errors.Select(e => new BaseError { PropertyMessage = e.Description });

			return response;
			
		}

		private string GenerateToken(IdentityUser identityUser, IList<string> roles)
		{
			var roleClaims = new List<Claim>();

			for(int i = 0;  i < roles.Count; i++)
			{
				roleClaims.Add(new Claim("roles", roles[i]));
			}

			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, identityUser.Id),
				new Claim(JwtRegisteredClaimNames.Email, identityUser.Email),
				new Claim(JwtRegisteredClaimNames.Name, identityUser.UserName)
			}.Union(roleClaims);

			var signingCredentials = new SigningCredentials(
					new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.Secret)),
					SecurityAlgorithms.HmacSha256
				);

			var token = new JwtSecurityToken(_appSettings.Issuer, _appSettings.Audience, claims, null, DateTime.UtcNow.AddHours(2), signingCredentials);

			string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

			return tokenValue;
		}
	}
}
