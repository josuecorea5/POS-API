using POS.Domain.Common;
using POS.Domain.Enums;

namespace POS.Domain.Entities
{
	public class RechargeSale : BaseAuditableEntity
	{
		public int SaleId { get; set; }
		public decimal Percentage { get; set; }
		public decimal NewTotal { get; set; }
		public RechargeSaleStatus RechargeSaleStatus { get; set; }
		public string Description { get; set; }
		public DateTime LimitDate { get; set; }
		public Sale Sale { get; set; } = null!;
	}
}
