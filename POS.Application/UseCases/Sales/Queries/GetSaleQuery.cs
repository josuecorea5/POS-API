using MediatR;
using POS.Application.Common;
using POS.Application.DTOs.Sales;

namespace POS.Application.UseCases.Sales.Queries
{
	public sealed record GetSaleQuery : IRequest<Response<SaleDto>>
	{
		public int Id { get; set; }
	}
}
