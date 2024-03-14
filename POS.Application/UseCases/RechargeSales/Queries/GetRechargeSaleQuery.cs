using MediatR;
using POS.Application.Common;
using POS.Application.DTOs.RechargeSales;

namespace POS.Application.UseCases.RechargeSales.Queries
{
	public sealed record GetRechargeSaleQuery : IRequest<Response<RechargeSaleDto>>
	{
		public int Id { get; set; }
	}
}
