using System.Data;

namespace POS.Application.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		IClientRepository ClientRepository { get; }
		IProductRepository ProductRepository { get; }
		ISaleRepository SaleRepository { get; }
		ISaleDetailRepository SaleDetailRepository { get; }
		ISchedulePaymentRepository SchedulePaymentRepository { get; }
		IRechargeSaleRepository RechargeSaleRepository { get; }
		Task<int> SaveChanges(CancellationToken cancellationToken);
		IDbTransaction BeginTransaction();
	}
}
