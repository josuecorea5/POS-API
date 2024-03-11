using MediatR;
using POS.Application.Common;
using POS.Application.DTOs.SchedulePayments;

namespace POS.Application.UseCases.SchedulePayments.Queries
{
	public sealed record GetAllSchedulePaymentsQuery : IRequest<Response<IEnumerable<SchedulePaymentDto>>>
	{
	}
}
