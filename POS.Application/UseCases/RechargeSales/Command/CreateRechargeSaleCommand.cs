using MediatR;
using POS.Application.Common;

namespace POS.Application.UseCases.RechargeSales.Command
{
	public sealed record CreateRechargeSaleCommand : IRequest<Response<bool>>
	{
		public int SaleId { get; set; }
		public decimal Percentage { get; set; }
		public string Description { get; set; }
		public DateTime LimitDate { get; set; }
	}
}
