using MediatR;
using POS.Application.Common;

namespace POS.Application.UseCases.RechargeSales.Command
{
	public sealed record CreateNewRechargeSaleCommand : IRequest<Response<bool>>
	{
		public int RechargeId { get; set; }
		public string Description { get; set; }
		public DateTime LimitDate { get; set; }
	}
}
