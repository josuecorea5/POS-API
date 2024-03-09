using AutoMapper;
using MediatR;
using POS.Application.Common;
using POS.Application.Interfaces;

namespace POS.Application.UseCases.SaleDetails.Commands
{
	public class UpdateSaleDetailHandler : IRequestHandler<UpdateSaleDetailCommand, Response<bool>>
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public UpdateSaleDetailHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Response<bool>> Handle(UpdateSaleDetailCommand request, CancellationToken cancellationToken)
		{
			var response = new Response<bool>();
			var product = await _unitOfWork.ProductRepository.GetById(request.ProductId);
			var sale = await _unitOfWork.SaleRepository.GetById(request.SaleId);

			if (product is null)
			{
				response.Message = "Product was not found";
				return response;
			}

			if(sale is null)
			{
				response.Message = "Sale was not found";
				return response;
			}

			using var transaction = _unitOfWork.BeginTransaction();

			try
			{
				var saleDetail = await _unitOfWork.SaleDetailRepository.GetById(request.Id);

				if (request.Discount is null)
				{
					request.Discount = saleDetail.Discount;
				}

				_mapper.Map(request, saleDetail);

				saleDetail.Total = saleDetail.UnitPrice * saleDetail.Quantity;

				if (saleDetail.Discount > 0 && saleDetail.Discount < 1)
				{
					decimal totalDiscount = (decimal)(saleDetail.Discount * saleDetail.Total);
					saleDetail.Total -= totalDiscount;
				}
				_unitOfWork.SaleDetailRepository.Update(saleDetail);
				await _unitOfWork.SaveChanges(cancellationToken);

				sale.Total = sale.SaleDetails.Sum(sd => sd.Total);

				await _unitOfWork.SaveChanges(cancellationToken);

				transaction.Commit();

			}
			catch(Exception ex)
			{
				transaction.Rollback();
				response.Message = ex.Message;
				return response;
			}

			response.Success = true;
			response.Message = "Detail sale updated successfully";

			return response;
		}
	}
}
