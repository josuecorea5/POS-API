﻿using POS.Application.Interfaces;
using POS.Infrastructure.Context;

namespace POS.Infrastructure.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly POSDbContext _context;
		public IClientRepository ClientRepository { get; }

		public UnitOfWork(POSDbContext context, IClientRepository clientRepository)
		{
			_context = context;
			ClientRepository = clientRepository;
		}

		public async Task<int> SaveChanges(CancellationToken cancellationToken)
		{
			return await _context.SaveChangesAsync(cancellationToken);
		}

		public void Dispose()
		{
			System.GC.SuppressFinalize(this);
		}
	}
}