using MediatR;
using POS.Application.Common;
using POS.Application.DTOs.Sales;

namespace POS.Application.UseCases.Sales.Queries
{
	public sealed record GetAllSalesQuery : IRequest<Response<IEnumerable<SaleDto>>>
	{
	}
}
