using AutoMapper;
using MediatR;
using POS.Application.Common;
using POS.Application.Interfaces;

namespace POS.Application.UseCases.SchedulePayments.Commands
{
	public class UpdateSchedulePaymentHandler : IRequestHandler<UpdateSchedulePaymentCommand, Response<bool>>
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public UpdateSchedulePaymentHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Response<bool>> Handle(UpdateSchedulePaymentCommand request, CancellationToken cancellationToken)
		{
			var response = new Response<bool>();
			var existSale = await _unitOfWork.SaleRepository.GetById(request.SaleId);
			var schedulePayment = await _unitOfWork.SchedulePaymentRepository.GetById(request.Id);

			if(existSale is null)
			{
				response.Message = "sale does not exist";
				return response;
			}

			if(request.InitialAmount >= existSale.Total)
			{
				response.Message = "Initial amount must be less than total";
				return response;
			}

			_mapper.Map(request, schedulePayment);

			schedulePayment.Amount = (decimal)existSale.Total;
			
			if(schedulePayment.InitialAmount > 0)
			{
				schedulePayment.AmountRemaining = schedulePayment.Amount - schedulePayment.InitialAmount;
			}else
			{
				schedulePayment.AmountRemaining = schedulePayment.Amount;
			}

			_unitOfWork.SchedulePaymentRepository.Update(schedulePayment);

			var result = await _unitOfWork.SaveChanges(cancellationToken);

			if(result > 0)
			{
				response.Success = true;
				response.Message = "Schedule payment updated";
			}

			return response;

		}
	}
}
