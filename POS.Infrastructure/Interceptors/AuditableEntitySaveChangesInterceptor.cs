using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using POS.Application.Interfaces;
using POS.Domain.Common;

namespace POS.Infrastructure.Interceptors
{
	public class AuditableEntitySaveChangesInterceptor : SaveChangesInterceptor
	{
		private readonly ICurrentUser _currentUser;

		public AuditableEntitySaveChangesInterceptor(ICurrentUser currentUser)
		{
			_currentUser = currentUser;
		}

		public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
		{
			UpdateEntities(eventData.Context);
			return base.SavingChangesAsync(eventData, result, cancellationToken);
		}

		public void UpdateEntities(DbContext? context)
		{
			if (context is null) return;

			foreach(var entity in context.ChangeTracker.Entries<BaseAuditableEntity>())
			{
				if(entity.State == EntityState.Added)
				{
					entity.Entity.CreatedBy = _currentUser.GetUserId();
					entity.Entity.Created = DateTime.UtcNow;
				}

				if (entity.State == EntityState.Modified)
				{
					entity.Entity.LastModifiedBy = _currentUser.GetUserId();
					entity.Entity.LastModified = DateTime.UtcNow;
				}
			}
		}
	}
}
