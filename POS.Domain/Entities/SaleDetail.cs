using POS.Domain.Common;

namespace POS.Domain.Entities
{
	public class SaleDetail : BaseAuditableEntity
	{
		public int SaleId { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
		public decimal Total { get; set; }
		public decimal? Discount { get; set; } = 0;
		public Sale Sale { get; set; } = null!;
		public Product Product { get; set; } = null!;
	}
}
