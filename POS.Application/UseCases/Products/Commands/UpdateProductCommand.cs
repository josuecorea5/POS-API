using MediatR;
using POS.Application.Common;
using POS.Domain.Enums;

namespace POS.Application.UseCases.Products.Commands
{
	public class UpdateProductCommand : IRequest<Response<bool>>
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public decimal UnitPrice { get; set; }
		public StatusEnum? Status { get; set; }
	}
}
