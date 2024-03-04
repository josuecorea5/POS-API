using MediatR;
using POS.Application.Common;
using POS.Domain.Enums;

namespace POS.Application.UseCases.Clients.Commands
{
	public class UpdateClientCommand : IRequest<Response<bool>>
	{
		public int Id { get; set; }
		public string FullName { get; set; }
		public StatusEnum Status { get; set; }
	}
}
