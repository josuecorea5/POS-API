using MediatR;
using POS.Application.Common;
using POS.Application.UseCases.SaleDetails.Commands;
using POS.Domain.Enums;

namespace POS.Application.UseCases.Sales.Commands
{
	public sealed record CreateSaleCommand : IRequest<Response<bool>>
	{
		public SaleStatus? SaleStatus { get; set; }
		public int ClientId { get; set; }
		public List<CreateSaleDetailCommand> SaleDetails { get; set; }
	}
}
