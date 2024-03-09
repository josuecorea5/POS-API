using AutoMapper;
using MediatR;
using POS.Application.Common;
using POS.Application.DTOs.Sales;
using POS.Application.Interfaces;

namespace POS.Application.UseCases.Sales.Queries
{
	public class GetSaleHandler : IRequestHandler<GetSaleQuery, Response<SaleDto>>
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public GetSaleHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Response<SaleDto>> Handle(GetSaleQuery request, CancellationToken cancellationToken)
		{
			var response = new Response<SaleDto>();

			var sale = await _unitOfWork.SaleRepository.GetById(request.Id);

			if(sale is null)
			{
				response.Message = "Sale was not found";
				return response;
			}

			response.Success = true;
			response.Data = _mapper.Map<SaleDto>(sale);

			return response;
		}
	}
}
