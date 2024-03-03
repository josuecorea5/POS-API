using POS.Domain.Common;
using POS.Domain.Enums;

namespace POS.Domain.Entities
{
	public class Client : BaseAuditableEntity
	{
		public string FullName { get; set; }
		public StatusEnum Status { get; set; }
	}
}
