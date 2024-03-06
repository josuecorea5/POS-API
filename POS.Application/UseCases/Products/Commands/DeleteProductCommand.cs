using MediatR;
using POS.Application.Common;

namespace POS.Application.UseCases.Products.Commands
{
	public sealed record DeleteProductCommand : IRequest<Response<bool>>
	{
        public int Id { get; set; }
    }
}
