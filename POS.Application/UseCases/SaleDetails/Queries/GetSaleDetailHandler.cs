using AutoMapper;
using MediatR;
using POS.Application.Common;
using POS.Application.DTOs.SaleDetails;
using POS.Application.Interfaces;

namespace POS.Application.UseCases.SaleDetails.Queries
{
	public class GetSaleDetailHandler : IRequestHandler<GetSaleDetailQuery, Response<SaleDetailDto>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public GetSaleDetailHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Response<SaleDetailDto>> Handle(GetSaleDetailQuery request, CancellationToken cancellationToken)
		{
			var response = new Response<SaleDetailDto>();
			var saleDetail = await _unitOfWork.SaleDetailRepository.GetById(request.Id);

			if(saleDetail is null)
			{
				response.Message = "Sale detail was not found";
				return response;
			}

			response.Success = true;
			response.Message = "Request successfully";
			response.Data = _mapper.Map<SaleDetailDto>(saleDetail);

			return response;
		}
	}
}
