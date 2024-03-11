using MediatR;
using POS.Application.Common;

namespace POS.Application.UseCases.SchedulePayments.Commands
{
	public sealed record UpdateSchedulePaymentCommand : IRequest<Response<bool>>
	{
		public int Id { get; set; }
		public int SaleId { get; set; }
		public decimal InitialAmount { get; set; }
		public DateTime LimitDate { get; set; }
	}
}
