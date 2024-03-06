namespace POS.Application.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		IClientRepository ClientRepository { get; }
		IProductRepository ProductRepository { get; }
		Task<int> SaveChanges(CancellationToken cancellationToken);
	}
}
