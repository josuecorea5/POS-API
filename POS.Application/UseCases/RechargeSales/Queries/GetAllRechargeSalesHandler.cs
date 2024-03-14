using AutoMapper;
using MediatR;
using POS.Application.Common;
using POS.Application.DTOs.RechargeSales;
using POS.Application.Interfaces;

namespace POS.Application.UseCases.RechargeSales.Queries
{
	public class GetAllRechargeSalesHandler : IRequestHandler<GetAllRechargeSalesQuery, Response<IEnumerable<RechargeSaleDto>>>
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public GetAllRechargeSalesHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Response<IEnumerable<RechargeSaleDto>>> Handle(GetAllRechargeSalesQuery request, CancellationToken cancellationToken)
		{
			var response = new Response<IEnumerable<RechargeSaleDto>>();
			var rechargeSales = await _unitOfWork.RechargeSaleRepository.GetAll();

			if(rechargeSales is null)
			{
				response.Message = "Something went wrong with the request";
				return response;
			}

			response.Success = true;
			response.Message = "request successfully";
			response.Data = _mapper.Map<IEnumerable<RechargeSaleDto>>(rechargeSales);

			return response;
		}
	}
}
