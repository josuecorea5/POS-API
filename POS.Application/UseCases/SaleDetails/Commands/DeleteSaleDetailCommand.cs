using MediatR;
using POS.Application.Common;

namespace POS.Application.UseCases.SaleDetails.Commands
{
	public sealed record DeleteSaleDetailCommand : IRequest<Response<bool>>
	{
		public int Id { get; set; }
	}
}
