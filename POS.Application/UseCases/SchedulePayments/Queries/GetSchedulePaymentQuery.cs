using MediatR;
using POS.Application.Common;
using POS.Application.DTOs.SchedulePayments;

namespace POS.Application.UseCases.SchedulePayments.Queries
{
	public class GetSchedulePaymentQuery : IRequest<Response<SchedulePaymentDto>>
	{
		public int Id { get; set; }
	}
}
