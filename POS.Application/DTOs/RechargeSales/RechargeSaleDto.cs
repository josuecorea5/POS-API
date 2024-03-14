using POS.Application.DTOs.Sales;
using POS.Domain.Enums;

namespace POS.Application.DTOs.RechargeSales
{
	public class RechargeSaleDto
	{
		public int Id { get; set; }
		public SaleDto Sale { get; set; }
		public decimal Percentage { get; set; }
		public decimal NewTotal { get; set; }
		public RechargeSaleStatus RechargeSaleStatus { get; set; }
		public string Description { get; set; }
		public DateTime LimitDate { get; set; }
	}
}
