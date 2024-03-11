using MediatR;
using POS.Application.Common;
using POS.Domain.Enums;

namespace POS.Application.UseCases.SchedulePayments.Commands
{
	public sealed record UpdateSchedulePaymentStatusCommand : IRequest<Response<bool>>
	{
		public int Id { get; set; }
		public SchedulePaymentStatus SchedulePaymentStatus { get; set; }
	}
}
