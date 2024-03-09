using AutoMapper;
using MediatR;
using POS.Application.Common;
using POS.Application.Interfaces;
using POS.Domain.Entities;

namespace POS.Application.UseCases.Sales.Commands
{
	public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, Response<bool>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public CreateSaleHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Response<bool>> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
		{
			var response = new Response<bool>();
			var existClient = await _unitOfWork.ClientRepository.GetById(request.ClientId);

			if (existClient is null)
			{
				response.Message = "Client not found";
				return response;
			}

			var sale = _mapper.Map<Sale>(request);
			sale.DateSale = DateTime.UtcNow;

			using var transaction = _unitOfWork.BeginTransaction();

			try
			{
				await _unitOfWork.SaleRepository.Insert(sale);
				await _unitOfWork.SaveChanges(cancellationToken);
				foreach(var item in request.SaleDetails)
				{
					var existProduct = await _unitOfWork.ProductRepository.GetById(item.ProductId);

					if(existProduct is null)
					{
						response.Message = "Product not found";
						return response;
					}
					var saleDetail = _mapper.Map<SaleDetail>(item);
					saleDetail.Total = saleDetail.UnitPrice * saleDetail.Quantity;
					saleDetail.SaleId = sale.Id;
					saleDetail.ProductId = existProduct.Id;
					if (saleDetail.Discount > 0 && saleDetail.Discount < 1)
					{
						decimal totalDiscount = (decimal)(saleDetail.Discount * saleDetail.Total);
						saleDetail.Total -= totalDiscount;
					}
					await _unitOfWork.SaleDetailRepository.Insert(saleDetail);
					await _unitOfWork.SaveChanges(cancellationToken);
				}

				sale.Total = sale.SaleDetails.Sum(x => x.Total);
				await _unitOfWork.SaveChanges(cancellationToken);

				transaction.Commit();
			}catch (Exception ex)
			{
				transaction.Rollback();
				response.Message = ex.Message;
				return response;
			}

			response.Success = true;
			response.Message = "Sale created successfully";

			return response;
		}
	}
}
