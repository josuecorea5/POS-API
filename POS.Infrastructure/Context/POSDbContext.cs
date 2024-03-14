using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using POS.Domain.Entities;
using POS.Domain.Enums;
using POS.Infrastructure.Interceptors;
using System.Reflection;

namespace POS.Infrastructure.Context
{
	public class POSDbContext : IdentityDbContext
	{
        private readonly AuditableEntitySaveChangesInterceptor _interceptor;
		private readonly SoftDeleteInterceptor _softDeleteInterceptor;
		public POSDbContext(DbContextOptions<POSDbContext> options, AuditableEntitySaveChangesInterceptor interceptor, SoftDeleteInterceptor softDeleteInterceptor) : base(options)
		{
			_interceptor = interceptor;
			_softDeleteInterceptor = softDeleteInterceptor;
		}

		public DbSet<Client> Clients { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Sale> Sales { get; set; }
		public DbSet<SaleDetail> SaleDetail { get; set; }
		public DbSet<SchedulePayment> SchedulePayment { get; set; }
		public DbSet<RechargeSale> RechargeSale { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Client>().ToTable("Client").HasQueryFilter(x => x.Status == StatusEnum.Active);
			builder.Entity<Product>().ToTable("Product").HasQueryFilter(x => x.Status == StatusEnum.Active);
			builder.Entity<Sale>().ToTable("Sale").HasQueryFilter(x => x.Status == StatusEnum.Active);
			builder.Entity<SaleDetail>().ToTable("SaleDetail").HasQueryFilter(x => x.Status == StatusEnum.Active);
			builder.Entity<SchedulePayment>().ToTable("SchedulePayment").HasQueryFilter(x => x.Status == StatusEnum.Active);
			builder.Entity<RechargeSale>().ToTable("RechargeSale").HasQueryFilter(x => x.Status == StatusEnum.Active);
			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			base.OnModelCreating(builder);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.AddInterceptors(_interceptor, _softDeleteInterceptor);
			optionsBuilder.EnableSensitiveDataLogging();
		}
		public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			return await base.SaveChangesAsync(cancellationToken);
		}
	}
}
