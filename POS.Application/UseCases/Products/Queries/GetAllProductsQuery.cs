using MediatR;
using POS.Application.Common;
using POS.Application.DTOs.Products;

namespace POS.Application.UseCases.Products.Queries
{
	public class GetAllProductsQuery : IRequest<Response<IEnumerable<ProductDto>>>
	{
	}
}
