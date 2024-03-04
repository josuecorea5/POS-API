using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using POS.Domain.Common;
using POS.Domain.Enums;

namespace POS.Infrastructure.Interceptors
{
	public class SoftDeleteInterceptor : SaveChangesInterceptor
	{
		public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
		{
			SoftDeleteEntities(eventData.Context);
			return base.SavingChangesAsync(eventData, result, cancellationToken);
		}

		public void SoftDeleteEntities(DbContext? context)
		{
			if (context is null) return;

			foreach(var entry in context.ChangeTracker.Entries())
			{
				if (entry is not { State: EntityState.Deleted, Entity: BaseEntity delete }) continue;
				entry.State = EntityState.Modified;
				delete.Status = StatusEnum.Inactive;
			}
		}
	}
}
