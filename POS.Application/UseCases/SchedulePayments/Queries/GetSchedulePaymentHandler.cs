using AutoMapper;
using MediatR;
using POS.Application.Common;
using POS.Application.DTOs.SchedulePayments;
using POS.Application.Interfaces;

namespace POS.Application.UseCases.SchedulePayments.Queries
{
	public class GetSchedulePaymentHandler : IRequestHandler<GetSchedulePaymentQuery, Response<SchedulePaymentDto>>
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public GetSchedulePaymentHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Response<SchedulePaymentDto>> Handle(GetSchedulePaymentQuery request, CancellationToken cancellationToken)
		{
			var response = new Response<SchedulePaymentDto>();
			var schedulePayment = await _unitOfWork.SchedulePaymentRepository.GetById(request.Id);

            if (schedulePayment is null)
            {
				response.Message = "Schedule payment not found";
				return response;
            }

			response.Success = true;
			response.Message = "request successfully";
			response.Data = _mapper.Map<SchedulePaymentDto>(schedulePayment);

			return response;
        }
	}
}
