using AutoMapper;
using MediatR;
using POS.Application.Common;
using POS.Application.Interfaces;
using POS.Domain.Enums;

namespace POS.Application.UseCases.RechargeSales.Command
{
	public class UpdateRechargeSaleStatusHandler : IRequestHandler<UpdateRechargeSaleStatusCommand, Response<bool>>
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public UpdateRechargeSaleStatusHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Response<bool>> Handle(UpdateRechargeSaleStatusCommand request, CancellationToken cancellationToken)
		{
			var response = new Response<bool>();
			var rechargeSale = await _unitOfWork.RechargeSaleRepository.GetById(request.Id);
			if(request.RechargeSaleStatus == RechargeSaleStatus.OVERDUE  && rechargeSale.LimitDate < DateTime.UtcNow)
			{
				response.Message = "The limit date has not been reached";
				return response;
			}

			var transaction = _unitOfWork.BeginTransaction();

			try
			{
				if (request.RechargeSaleStatus == RechargeSaleStatus.CLOSED)
				{
					var sale = await _unitOfWork.SaleRepository.GetById(rechargeSale.SaleId);
					var scheduledPayment = await _unitOfWork.SchedulePaymentRepository.GetSchedulePaymentBySaleId(rechargeSale.SaleId);
					sale.SaleStatus = SaleStatus.CLOSED;
					sale.Total = rechargeSale.NewTotal;
					scheduledPayment.SchedulePaymentStatus = SchedulePaymentStatus.COMPLETED;
					_mapper.Map(request, rechargeSale);
					_unitOfWork.RechargeSaleRepository.Update(rechargeSale);
					_unitOfWork.SchedulePaymentRepository.Update(scheduledPayment);
					_unitOfWork.SaleRepository.Update(sale);
					await _unitOfWork.SaveChanges(cancellationToken);
				}
				else
				{
					_mapper.Map(request, rechargeSale);
					_unitOfWork.RechargeSaleRepository.Update(rechargeSale);
					await _unitOfWork.SaveChanges(cancellationToken);
				}

				transaction.Commit();
			}
			catch(Exception ex)
			{
				transaction.Rollback();
				response.Message = ex.Message;
				return response;
			}

			response.Success = true;
			response.Message = "Recharge sale status updated successfully";

			return response;
		}
	}
}
