using POS.Domain.Common;

namespace POS.Domain.Entities
{
	public class Client : BaseAuditableEntity
	{
		public string FullName { get; set; }
		public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
	}
}
