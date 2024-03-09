using POS.Domain.Common;
using POS.Domain.Enums;

namespace POS.Domain.Entities
{
	public class Sale : BaseAuditableEntity
	{
		public DateTime DateSale { get; set; }
		public decimal? Total { get; set; }
		public SaleStatus SaleStatus { get; set; }
		public int ClientId { get; set; }
		public Client Client { get; set; } = null!;
		public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
	}
}
