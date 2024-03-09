using MediatR;
using POS.Application.Common;
using POS.Domain.Enums;

namespace POS.Application.UseCases.Sales.Commands
{
	public sealed record UpdateSaleCommand : IRequest<Response<bool>>
	{
		public int Id { get; set; }
		public DateTime DateSale { get; set; }
		public SaleStatus? SaleStatus { get; set; }
		public int ClientId { get; set; }
	}
}
