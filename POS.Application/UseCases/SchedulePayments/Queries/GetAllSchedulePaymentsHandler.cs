using AutoMapper;
using MediatR;
using POS.Application.Common;
using POS.Application.DTOs.SchedulePayments;
using POS.Application.Interfaces;

namespace POS.Application.UseCases.SchedulePayments.Queries
{
	public class GetAllSchedulePaymentsHandler : IRequestHandler<GetAllSchedulePaymentsQuery, Response<IEnumerable<SchedulePaymentDto>>>
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public GetAllSchedulePaymentsHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Response<IEnumerable<SchedulePaymentDto>>> Handle(GetAllSchedulePaymentsQuery request, CancellationToken cancellationToken)
		{
			var response = new Response<IEnumerable<SchedulePaymentDto>>();
			var schedulePayments = await _unitOfWork.SchedulePaymentRepository.GetAll();

			if(schedulePayments is null)
			{
				response.Message = "something went wrong with the request";
				return response;
			}

			response.Success = true;
			response.Data = _mapper.Map<IEnumerable<SchedulePaymentDto>>(schedulePayments);
			response.Message = "request successfully";

			return response;
		}
	}
}
