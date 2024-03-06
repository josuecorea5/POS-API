using MediatR;
using POS.Application.Common;

namespace POS.Application.UseCases.Products.Commands
{
	public sealed record CreateProductCommand : IRequest<Response<bool>>
	{
		public string Name { get; set; }
		public decimal UnitPrice { get; set; }
	}
}
