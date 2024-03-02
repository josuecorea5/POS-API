using POS.Application.Common;
using POS.Application.Common.Exceptions;
using System.Net;
using System.Text.Json;

namespace POS.App.Modules.GlobalException
{
	public class GlobalExceptionHandler : IMiddleware
	{
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			try
			{
				await next(context);
			}catch(ValidationCustomException ex)
			{
				context.Response.ContentType = "application/json";
				context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
				await JsonSerializer.SerializeAsync(context.Response.Body,
						new Response<Object> { Message = "Validation errors", Errors = ex.Errors });
			}
			catch (Exception ex)
			{
				string message = ex.Message.ToString();
				context.Request.ContentType = "application/json";
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

				var response = new Response<object>() { Message = message };

				await JsonSerializer.SerializeAsync(context.Response.Body, response);
			}
		}
	}
}
