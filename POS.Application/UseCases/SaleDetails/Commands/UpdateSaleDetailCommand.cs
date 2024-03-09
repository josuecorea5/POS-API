using MediatR;
using POS.Application.Common;

namespace POS.Application.UseCases.SaleDetails.Commands
{
	public sealed record UpdateSaleDetailCommand : IRequest<Response<bool>>
	{
		public int Id { get; set; }
		public int SaleId { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
		public decimal? Discount { get; set; }
	}
}
