using POS.Domain.Enums;

namespace POS.Domain.Common
{
	public class BaseEntity
	{
        public int Id { get; set; }
		public StatusEnum Status { get; set; }

	}
}
