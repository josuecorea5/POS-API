using AutoMapper;
using MediatR;
using POS.Application.Common;
using POS.Application.DTOs.Products;
using POS.Application.Interfaces;

namespace POS.Application.UseCases.Products.Queries
{
	public class GetProductHandler : IRequestHandler<GetProductQuery, Response<ProductDto>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public GetProductHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Response<ProductDto>> Handle(GetProductQuery request, CancellationToken cancellationToken)
		{
			var response = new Response<ProductDto>();
			var product = await _unitOfWork.ProductRepository.GetById(request.Id);

			if (product is null)
			{
				response.Message = "Product was not found";
				return response;
			}

			response.Success = true;
			response.Message = "Request successfully";
			response.Data = _mapper.Map<ProductDto>(product);
			response.Message = "Request successfully";

			return response;
		}
	}
}
