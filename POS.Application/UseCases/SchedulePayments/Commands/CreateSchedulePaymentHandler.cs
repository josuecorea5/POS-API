using AutoMapper;
using MediatR;
using POS.Application.Common;
using POS.Application.Interfaces;
using POS.Domain.Entities;
using POS.Domain.Enums;

namespace POS.Application.UseCases.SchedulePayments.Commands
{
	public class CreateSchedulePaymentHandler : IRequestHandler<CreateSchedulePaymentCommand, Response<bool>>
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public CreateSchedulePaymentHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Response<bool>> Handle(CreateSchedulePaymentCommand request, CancellationToken cancellationToken)
		{
			var response = new Response<bool>();
			var sale = await _unitOfWork.SaleRepository.GetById(request.SaleId);

			if(sale is null)
			{
				response.Message = "sale was not found";
				return response;
			}

			if(sale.SaleStatus == SaleStatus.CLOSED)
			{
				response.Message = "You can not create a schedule payment for a closed sale";
				return response;
			}
			var existSchedulePayment = await _unitOfWork.SchedulePaymentRepository.GetSchedulePaymentBySaleId(request.SaleId);

			if (existSchedulePayment != null)
			{
				response.Message = "There is a schedule payment for this sale";
				return response;
			}

			var schedulePayment = _mapper.Map<SchedulePayment>(request);
			schedulePayment.CreatedDate = DateTime.UtcNow;
			schedulePayment.Amount = (decimal)sale.Total;

			if (schedulePayment.InitialAmount > 0)
			{
				schedulePayment.AmountRemaining = schedulePayment.Amount - schedulePayment.InitialAmount;
			}else
			{
				schedulePayment.AmountRemaining = schedulePayment.Amount;
			}

			await _unitOfWork.SchedulePaymentRepository.Insert(schedulePayment);
			var result = await _unitOfWork.SaveChanges(cancellationToken);

			if(result > 0)
			{
				response.Success = true;
				response.Message = "Schedule payment created successfully";
			}else
			{
				response.Message = "Something went wrong while creating the schedule payment";
			}

			return response;
		}
	}
}
