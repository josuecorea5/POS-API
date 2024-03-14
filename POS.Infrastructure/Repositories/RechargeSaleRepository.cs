using Microsoft.EntityFrameworkCore;
using POS.Application.Interfaces;
using POS.Domain.Entities;
using POS.Infrastructure.Context;

namespace POS.Infrastructure.Repositories
{
	public class RechargeSaleRepository : IRechargeSaleRepository
	{
		private readonly POSDbContext _context;

		public RechargeSaleRepository(POSDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<RechargeSale>> GetAll()
		{
			return await _context.RechargeSale.Include(rs => rs.Sale).Include(rs => rs.Sale.Client).ToListAsync();
		}

		public async Task<RechargeSale> GetById(int id)
		{
			return await _context.RechargeSale.Include(rs => rs.Sale).Include(rs => rs.Sale.Client).FirstOrDefaultAsync(rs => rs.Id.Equals(id));
		}

		public async Task Insert(RechargeSale entity)
		{
			await _context.RechargeSale.AddAsync(entity);
		}

		public void Update(RechargeSale entity)
		{
			_context.RechargeSale.Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;
		}
		public void Delete(RechargeSale entity)
		{
			_context.RechargeSale.Remove(entity);
		}
		public async Task<RechargeSale> GetRechargeSaleBySaleId(int id)
		{
			return await _context.RechargeSale.FirstOrDefaultAsync(sp => sp.SaleId == id);
		}
	}
}
