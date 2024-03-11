using Microsoft.EntityFrameworkCore;
using POS.Application.Interfaces;
using POS.Domain.Entities;
using POS.Infrastructure.Context;

namespace POS.Infrastructure.Repositories
{
	public class SchedulePaymentRespository : ISchedulePaymentRepository
	{
		private readonly POSDbContext _context;

		public SchedulePaymentRespository(POSDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<SchedulePayment>> GetAll()
		{
			return await _context.SchedulePayment.Include(sp => sp.Sale).Include(s => s.Sale.Client).ToListAsync();
		}
		public async Task<SchedulePayment> GetById(int id)
		{
			return await _context.SchedulePayment.Include(sp => sp.Sale).Include(sp => sp.Sale.Client).FirstOrDefaultAsync(sp => sp.Id.Equals(id));
		}
		public async Task Insert(SchedulePayment entity)
		{
			await _context.SchedulePayment.AddAsync(entity);
		}
		public void Update(SchedulePayment entity)
		{
			_context.SchedulePayment.Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;
		}
		public void Delete(SchedulePayment entity)
		{
			_context.SchedulePayment.Remove(entity);
		}

		public async Task<SchedulePayment> GetSchedulePaymentBySaleId(int id)
		{
			return await _context.SchedulePayment.FirstOrDefaultAsync(sp => sp.SaleId == id);
		}
	}
}
