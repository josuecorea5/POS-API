using MediatR;
using POS.Application.Common;
using POS.Application.DTOs.SaleDetails;

namespace POS.Application.UseCases.SaleDetails.Queries
{
	public sealed record GetSaleDetailQuery : IRequest<Response<SaleDetailDto>>
	{
		public int Id { get; set; }
	}
}
