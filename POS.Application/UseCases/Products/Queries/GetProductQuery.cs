using MediatR;
using POS.Application.Common;
using POS.Application.DTOs.Products;

namespace POS.Application.UseCases.Products.Queries
{
	public class GetProductQuery : IRequest<Response<ProductDto>>
	{
        public int Id { get; set; }
    }
}
