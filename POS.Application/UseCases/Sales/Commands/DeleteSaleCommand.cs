using MediatR;
using POS.Application.Common;

namespace POS.Application.UseCases.Sales.Commands
{
	public sealed record DeleteSaleCommand : IRequest<Response<bool>>
	{
		public int Id { get; set; }
	}
}
