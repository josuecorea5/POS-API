using AutoMapper;
using MediatR;
using POS.Application.Common;
using POS.Application.DTOs.Sales;
using POS.Application.Interfaces;

namespace POS.Application.UseCases.Sales.Queries
{
	public class GetAllSalesHandler : IRequestHandler<GetAllSalesQuery, Response<IEnumerable<SaleDto>>>
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public GetAllSalesHandler(IMapper mapper, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}


		public async Task<Response<IEnumerable<SaleDto>>> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
		{
			var response = new Response<IEnumerable<SaleDto>>();
			var sales = await _unitOfWork.SaleRepository.GetAll();

			if (sales is null)
			{
				response.Message = "Something went wrong with the request";
				return response;
			}

			response.Success = true;
			response.Data = _mapper.Map<IEnumerable<SaleDto>>(sales);
			response.Message = "Request successfully";

			return response;
		}
	}
}
