using POS.Domain.Entities;

namespace POS.Application.Interfaces
{
	public interface IRechargeSaleRepository : IGenericRepository<RechargeSale>
	{
		Task<RechargeSale> GetRechargeSaleBySaleId(int id);
	}
}
