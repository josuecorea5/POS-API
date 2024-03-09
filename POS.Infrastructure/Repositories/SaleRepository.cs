using Microsoft.EntityFrameworkCore;
using POS.Application.Interfaces;
using POS.Domain.Entities;
using POS.Infrastructure.Context;

namespace POS.Infrastructure.Repositories
{
	public class SaleRepository : ISaleRepository
	{
		private readonly POSDbContext _context;

		public SaleRepository(POSDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Sale>> GetAll()
		{
			return await _context.Sales.Include("Client").Include("SaleDetails").ToListAsync();
		}

		public async Task<Sale> GetById(int id)
		{
			return await _context.Sales.Include("Client").Include("SaleDetails").FirstOrDefaultAsync(s => s.Id.Equals(id));
		}
		public async Task Insert(Sale entity)
		{
			await _context.Sales.AddAsync(entity);
		}
		public void Update(Sale entity)
		{
			_context.Sales.Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;
		}
		public void Delete(Sale entity)
		{
			_context.Sales.Remove(entity);
		}
	}
}
