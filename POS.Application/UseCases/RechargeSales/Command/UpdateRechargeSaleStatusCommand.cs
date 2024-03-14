using MediatR;
using POS.Application.Common;
using POS.Domain.Enums;

namespace POS.Application.UseCases.RechargeSales.Command
{
	public sealed record UpdateRechargeSaleStatusCommand : IRequest<Response<bool>>
	{
		public int Id { get; set; }
		public RechargeSaleStatus RechargeSaleStatus { get; set; }
	}
}
