using POS.Application.DTOs.Clients;
using POS.Application.DTOs.SaleDetails;
using POS.Domain.Enums;

namespace POS.Application.DTOs.Sales
{
	public class SaleDto
	{
		public int Id { get; set; }
		public SaleStatus SaleStatus { get; set; }
		public decimal Total { get; set; }
		public ClientDto? Client { get; set; }
		public ICollection<SaleDetailDto>? SaleDetails { get; set; }
	}
}
