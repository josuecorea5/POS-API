using MediatR;
using POS.Application.Common;
using POS.Application.Interfaces;

namespace POS.Application.UseCases.Sales.Commands
{
	public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, Response<bool>>
	{
		private readonly IUnitOfWork _unitOfWork;

		public DeleteSaleHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<Response<bool>> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
		{
			var response = new Response<bool>();
			var sale = await _unitOfWork.SaleRepository.GetById(request.Id);

			_unitOfWork.SaleRepository.Delete(sale);
			var result = await _unitOfWork.SaveChanges(cancellationToken);

			if(result > 0)
			{
				response.Success = true;
				response.Message = "Sale deleted successfully";
			}else
			{
				response.Message = "Something went wrong while updateing the register";
			}

			return response;
		}
	}
}
