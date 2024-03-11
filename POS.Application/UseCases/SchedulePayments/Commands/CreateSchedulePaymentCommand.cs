using MediatR;
using POS.Application.Common;

namespace POS.Application.UseCases.SchedulePayments.Commands
{
	public sealed record CreateSchedulePaymentCommand : IRequest<Response<bool>>
	{
		public int SaleId { get; set; }
		public decimal InitialAmount { get; set; }
		public DateTime LimitDate { get; set; }
	}
}
