using MediatR;
using POS.Application.Common;
using POS.Application.Interfaces;
using POS.Domain.Enums;

namespace POS.Application.UseCases.RechargeSales.Command
{
	public class DeleteRechargeSaleHandler : IRequestHandler<DeleteRechargeSaleCommand, Response<bool>>
	{
		private readonly IUnitOfWork _unitOfWork;

		public DeleteRechargeSaleHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<Response<bool>> Handle(DeleteRechargeSaleCommand request, CancellationToken cancellationToken)
		{
			var response = new Response<bool>();

			var rechargeSale = await _unitOfWork.RechargeSaleRepository.GetById(request.Id);

			if(rechargeSale.RechargeSaleStatus != RechargeSaleStatus.PENDING)
			{
				response.Message = "You cannot delete an active recharge sale";
				return response;
			}

			_unitOfWork.RechargeSaleRepository.Delete(rechargeSale);
			var result = await _unitOfWork.SaveChanges(cancellationToken);

			if(result > 0)
			{
				response.Success = true;
				response.Message = "recharge sale deleted successfully";
			}else
			{
				response.Message = "something went wrong while deleting the recharge sale";
			}

			return response;
		}
	}
}
