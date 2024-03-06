using POS.Domain.Common;

namespace POS.Domain.Entities
{
	public class Product : BaseAuditableEntity
	{
		public string Name { get; set; }
		public decimal UnitPrice { get; set; }
	}
}
