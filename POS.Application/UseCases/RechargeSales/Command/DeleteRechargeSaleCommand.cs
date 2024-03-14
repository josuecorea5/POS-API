using MediatR;
using POS.Application.Common;

namespace POS.Application.UseCases.RechargeSales.Command
{
	public sealed record DeleteRechargeSaleCommand : IRequest<Response<bool>>
	{
		public int Id { get; set; }
	}
}
