using Microsoft.EntityFrameworkCore.Storage;
using POS.Application.Interfaces;
using POS.Infrastructure.Context;
using System.Data;

namespace POS.Infrastructure.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly POSDbContext _context;
		public IClientRepository ClientRepository { get; }

		public IProductRepository ProductRepository { get; }
		public ISaleRepository SaleRepository { get; }
		public ISaleDetailRepository SaleDetailRepository { get; }

		public UnitOfWork(POSDbContext context, IClientRepository clientRepository, IProductRepository productRepository, ISaleRepository saleRepository, ISaleDetailRepository saleDetailRepository)
		{
			_context = context;
			ClientRepository = clientRepository;
			ProductRepository = productRepository;
			SaleRepository = saleRepository;
			SaleDetailRepository = saleDetailRepository;
		}

		public async Task<int> SaveChanges(CancellationToken cancellationToken)
		{
			return await _context.SaveChangesAsync(cancellationToken);
		}

		public void Dispose()
		{
			System.GC.SuppressFinalize(this);
		}

		public IDbTransaction BeginTransaction()
		{
			var transaction = _context.Database.BeginTransaction();

			return transaction.GetDbTransaction();
		}
	}
}
