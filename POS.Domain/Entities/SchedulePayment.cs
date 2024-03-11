using POS.Domain.Common;
using POS.Domain.Enums;

namespace POS.Domain.Entities
{
	public class SchedulePayment : BaseAuditableEntity
	{
		public int SaleId { get; set; }
		public decimal Amount { get; set; }
		public decimal InitialAmount { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime LimitDate { get; set; }
		public decimal AmountRemaining { get; set; }
		public SchedulePaymentStatus SchedulePaymentStatus { get; set; }
		public Sale Sale { get; set; } = null!;
	}
}
