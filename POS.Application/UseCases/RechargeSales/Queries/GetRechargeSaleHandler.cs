using AutoMapper;
using MediatR;
using POS.Application.Common;
using POS.Application.DTOs.RechargeSales;
using POS.Application.Interfaces;

namespace POS.Application.UseCases.RechargeSales.Queries
{
	public class GetRechargeSaleHandler : IRequestHandler<GetRechargeSaleQuery, Response<RechargeSaleDto>>
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public GetRechargeSaleHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Response<RechargeSaleDto>> Handle(GetRechargeSaleQuery request, CancellationToken cancellationToken)
		{
			var response = new Response<RechargeSaleDto>();

			var rechargeSale = await _unitOfWork.RechargeSaleRepository.GetById(request.Id);

			if(rechargeSale is null)
			{
				response.Message = "Recharge sale was not found";
				return response;
			}

			response.Success = true;
			response.Data = _mapper.Map<RechargeSaleDto>(rechargeSale);
			response.Message = "request successfully";

			return response;
		}
	}
}
