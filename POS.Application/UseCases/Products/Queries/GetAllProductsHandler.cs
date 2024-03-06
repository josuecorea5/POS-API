using AutoMapper;
using MediatR;
using POS.Application.Common;
using POS.Application.DTOs.Products;
using POS.Application.Interfaces;

namespace POS.Application.UseCases.Products.Queries
{
	public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, Response<IEnumerable<ProductDto>>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public GetAllProductsHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Response<IEnumerable<ProductDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
		{
			var response = new Response<IEnumerable<ProductDto>>();
			var products = await _unitOfWork.ProductRepository.GetAll();

			if(products is null)
			{
				response.Success = false;
				response.Message = "Something went wrong with the request";

				return response;
			}

			response.Success = true;
			response.Data = _mapper.Map<IEnumerable<ProductDto>>(products);
			response.Message = "Request successfully";

			return response;
		}
	}
}
