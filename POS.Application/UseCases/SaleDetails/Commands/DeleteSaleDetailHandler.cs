using MediatR;
using POS.Application.Common;
using POS.Application.Interfaces;
using POS.Domain.Enums;

namespace POS.Application.UseCases.SaleDetails.Commands
{
	public class DeleteSaleDetailHandler : IRequestHandler<DeleteSaleDetailCommand, Response<bool>>
	{
		private readonly IUnitOfWork _unitOfWork;

		public DeleteSaleDetailHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<Response<bool>> Handle(DeleteSaleDetailCommand request, CancellationToken cancellationToken)
		{
			var response = new Response<bool>();
			var saleDetail = await _unitOfWork.SaleDetailRepository.GetById(request.Id);

			using var transaction = _unitOfWork.BeginTransaction();

			try
			{
				_unitOfWork.SaleDetailRepository.Delete(saleDetail);
				await _unitOfWork.SaveChanges(cancellationToken);

				var sale = await _unitOfWork.SaleRepository.GetById(saleDetail.SaleId);
				var activeSales = sale.SaleDetails.Where(sd => sd.Status == StatusEnum.Active);
				if (!activeSales.Any())
				{
					_unitOfWork.SaleRepository.Delete(sale);
				}
				else
				{
					sale.Total = activeSales.Sum(x => x.Total);
				}

				await _unitOfWork.SaveChanges(cancellationToken);

				transaction.Commit();
			}
			catch (Exception ex)
			{
				transaction.Rollback();
				response.Message = ex.Message;
				return response;
			}

			response.Success = true;
			response.Message = "Sale detail deleted successfully";

			return response;
		}
	}
}
