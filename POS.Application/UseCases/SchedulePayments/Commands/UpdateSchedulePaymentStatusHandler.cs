using MediatR;
using POS.Application.Common;
using POS.Application.Interfaces;
using POS.Domain.Enums;

namespace POS.Application.UseCases.SchedulePayments.Commands
{
	public class UpdateSchedulePaymentStatusHandler : IRequestHandler<UpdateSchedulePaymentStatusCommand, Response<bool>>
	{
		private readonly IUnitOfWork _unitOfWork;

		public UpdateSchedulePaymentStatusHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<Response<bool>> Handle(UpdateSchedulePaymentStatusCommand request, CancellationToken cancellationToken)
		{
			var response = new Response<bool>();
			var schedulePayment = await _unitOfWork.SchedulePaymentRepository.GetById(request.Id);
			var existSale = await _unitOfWork.SaleRepository.GetById(schedulePayment.SaleId);

			if(existSale is  null)
			{
				response.Message = "sale was not found";
				return response;
			}

			if(request.SchedulePaymentStatus == SchedulePaymentStatus.OVERDUE && schedulePayment.LimitDate < DateTime.UtcNow)
			{
				response.Message = "You cannot mark a scheduled payment as overdue prior the limit date";
				return response;
			}

			using var transaction = _unitOfWork.BeginTransaction();

			try
			{
				if (request.SchedulePaymentStatus == SchedulePaymentStatus.COMPLETED)
				{
					schedulePayment.AmountRemaining = 0;
					schedulePayment.SchedulePaymentStatus = SchedulePaymentStatus.COMPLETED;
					existSale.SaleStatus = SaleStatus.CLOSED;
					_unitOfWork.SchedulePaymentRepository.Update(schedulePayment);
					_unitOfWork.SaleRepository.Update(existSale);
				}
				else
				{
					schedulePayment.SchedulePaymentStatus = request.SchedulePaymentStatus;
					_unitOfWork.SchedulePaymentRepository.Update(schedulePayment);
				}
				await _unitOfWork.SaveChanges(cancellationToken);

				transaction.Commit();
			}
			catch(Exception ex)
			{
				transaction.Rollback();
				response.Message = ex.Message;
			}

			response.Success = true;
			response.Message = "Status payment successfully updated";

			return response;
		}
	}
}
