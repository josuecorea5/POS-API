using AutoMapper;
using MediatR;
using POS.Application.Common;
using POS.Application.Interfaces;
using POS.Domain.Enums;

namespace POS.Application.UseCases.RechargeSales.Command
{
	public class UpdateRechargeSaleHandler : IRequestHandler<UpdateRechargeSaleCommand, Response<bool>>
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public UpdateRechargeSaleHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Response<bool>> Handle(UpdateRechargeSaleCommand request, CancellationToken cancellationToken)
		{
			var response = new Response<bool>();

			var schedulePayment = await _unitOfWork.SchedulePaymentRepository.GetSchedulePaymentBySaleId(request.SaleId);
			var rechargeSale = await _unitOfWork.RechargeSaleRepository.GetById(request.Id);

			if (schedulePayment is null)
			{
				response.Message = "This sale does not have a scheduled payment to apply a recharge";
				return response;
			}

			if (schedulePayment.SchedulePaymentStatus != SchedulePaymentStatus.OVERDUE)
			{
				response.Message = "The scheduled payment status is not overdue";
				return response;
			}

			_mapper.Map(request, rechargeSale);

			rechargeSale.NewTotal = schedulePayment.Amount + (schedulePayment.Amount * rechargeSale.Percentage);

			_unitOfWork.RechargeSaleRepository.Update(rechargeSale);

			var result = await _unitOfWork.SaveChanges(cancellationToken);

			if(result > 0)
			{
				response.Success = true;
				response.Message = "Recharge sale updated successfully";
			}else
			{
				response.Message = "Something went wrong while updating the recharge sale";
			}

			return response;
		}
	}
}
