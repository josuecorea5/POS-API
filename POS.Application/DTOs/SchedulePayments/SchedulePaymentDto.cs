using POS.Application.DTOs.Sales;
using POS.Domain.Enums;

namespace POS.Application.DTOs.SchedulePayments
{
	public class SchedulePaymentDto
	{
		public int Id { get; set; }
		public SaleDto? Sale { get; set; }
		public decimal Amount { get; set; }
		public decimal InitialAmount { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime LimitDate { get; set; }
		public decimal AmountRemaining { get; set; }
		public SchedulePaymentStatus? SchedulePaymentStatus { get; set; }
	}
}
