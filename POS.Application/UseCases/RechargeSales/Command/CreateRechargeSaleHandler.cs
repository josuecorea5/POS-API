using AutoMapper;
using MediatR;
using POS.Application.Common;
using POS.Application.Interfaces;
using POS.Domain.Entities;
using POS.Domain.Enums;

namespace POS.Application.UseCases.RechargeSales.Command
{
	public class CreateRechargeSaleHandler : IRequestHandler<CreateRechargeSaleCommand, Response<bool>>
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public CreateRechargeSaleHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Response<bool>> Handle(CreateRechargeSaleCommand request, CancellationToken cancellationToken)
		{
			var response = new Response<bool>();

			var schedulePayment = await _unitOfWork.SchedulePaymentRepository.GetSchedulePaymentBySaleId(request.SaleId);
			var existRecharSale = await _unitOfWork.RechargeSaleRepository.GetRechargeSaleBySaleId(request.SaleId);

			if(existRecharSale != null && existRecharSale.Status == StatusEnum.Active)
			{
				response.Success = false;
				response.Message = "There is a peding recharge for this sale";
				return response;
			}

			if(schedulePayment is null)
			{
				response.Message = "This sale does not have a scheduled payment to apply a recharge";
				return response;
			}

			if(schedulePayment.SchedulePaymentStatus != SchedulePaymentStatus.OVERDUE)
			{
				response.Message = "The scheduled payment status is not overdue";
				return response;
			}

			var rechargeSale = _mapper.Map<RechargeSale>(request);
			rechargeSale.NewTotal = schedulePayment.Amount + (schedulePayment.Amount * rechargeSale.Percentage);

			await _unitOfWork.RechargeSaleRepository.Insert(rechargeSale);
			var result = await _unitOfWork.SaveChanges(cancellationToken);

			if(result > 0)
			{
				response.Success = true;
				response.Message = "Recharge sale created successfully";
			}else
			{
				response.Message = "Something went wrong while creating the recharge sale";
			}

			return response;
		}
	}
}
