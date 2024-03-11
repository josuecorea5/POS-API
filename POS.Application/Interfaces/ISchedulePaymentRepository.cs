using POS.Domain.Entities;

namespace POS.Application.Interfaces
{
	public interface ISchedulePaymentRepository : IGenericRepository<SchedulePayment>
	{
		Task<SchedulePayment> GetSchedulePaymentBySaleId(int id);
	}
}
