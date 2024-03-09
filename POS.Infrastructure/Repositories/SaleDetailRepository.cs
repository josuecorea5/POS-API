using Microsoft.EntityFrameworkCore;
using POS.Application.Interfaces;
using POS.Domain.Entities;
using POS.Infrastructure.Context;

namespace POS.Infrastructure.Repositories
{
	public class SaleDetailRepository : ISaleDetailRepository
	{
		private readonly POSDbContext _context;

		public SaleDetailRepository(POSDbContext dbContext)
		{
			_context = dbContext;
		}
		public async Task<IEnumerable<SaleDetail>> GetAll()
		{
			return await _context.SaleDetail.ToListAsync();
		}

		public async Task<SaleDetail> GetById(int id)
		{
			return await _context.SaleDetail.FindAsync(id);
		}
		public async Task Insert(SaleDetail entity)
		{
			await _context.SaleDetail.AddAsync(entity);
		}
		public void Update(SaleDetail entity)
		{
			_context.SaleDetail.Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;
		}

		public void Delete(SaleDetail entity)
		{
			_context.SaleDetail.Remove(entity);
		}
	}
}
