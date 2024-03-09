using POS.Application.DTOs.Products;

namespace POS.Application.DTOs.SaleDetails
{
	public class SaleDetailDto
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
		public decimal Total { get; set; }
		public decimal? Discount { get; set; }
	}
}
