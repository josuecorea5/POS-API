﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Client>().ToTable("Client").HasQueryFilter(x => x.Status == StatusEnum.Active);
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
