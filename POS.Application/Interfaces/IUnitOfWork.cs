﻿namespace POS.Application.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		IClientRepository ClientRepository { get; }
		Task<int> SaveChanges(CancellationToken cancellationToken);
	}
}
