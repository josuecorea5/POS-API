using Microsoft.EntityFrameworkCore;
using POS.Application.Interfaces;
using POS.Domain.Entities;
using POS.Domain.Enums;
using POS.Infrastructure.Context;

namespace POS.Infrastructure.Repositories
{
	public class ClientRepository : IClientRepository
	{
		private readonly POSDbContext _context;

		public ClientRepository(POSDbContext context)
		{
			_context = context;
		}
		public async Task Insert(Client entity)
		{
			await _context.AddAsync(entity);
		}

		public void Update(Client entity)
		{
			_context.Clients.Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;
		}

		public void Delete(Client entity)
		{
			_context.Clients.Remove(entity);
		}

		public async Task<IEnumerable<Client>> GetAll()
		{
			return await _context.Clients.ToListAsync();
		}

		public async Task<Client> GetById(int id)
		{
			return await _context.Clients.FindAsync(id);
		}

	}
}
