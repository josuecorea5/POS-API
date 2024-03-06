using Microsoft.EntityFrameworkCore;
using POS.Application.Interfaces;
using POS.Domain.Entities;
using POS.Infrastructure.Context;

namespace POS.Infrastructure.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly POSDbContext _context;

		public ProductRepository(POSDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Product>> GetAll()
		{
			return await _context.Products.ToListAsync();
		}

		public async Task<Product> GetById(int id)
		{
			return await _context.Products.FindAsync(id);
		}

		public async Task Insert(Product entity)
		{
			await _context.Products.AddAsync(entity);
		}

		public void Update(Product entity)
		{
			_context.Products.Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;
		}
		public void Delete(Product entity)
		{
			_context.Products.Remove(entity);
		}
	}
}
