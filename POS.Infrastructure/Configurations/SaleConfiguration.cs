using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;
using POS.Domain.Enums;

namespace POS.Infrastructure.Configurations
{
	public class SaleConfiguration : IEntityTypeConfiguration<Sale>
	{
		public void Configure(EntityTypeBuilder<Sale> builder)
		{
			builder.Property(s => s.DateSale)
				.IsRequired()
				.HasDefaultValue(DateTime.UtcNow);
			builder.Property(s => s.Total)
				.HasPrecision(16, 2);
			builder.Property(s => s.SaleStatus)
				.IsRequired();
			builder.Property(s => s.Status)
				.HasDefaultValue(StatusEnum.Active);
			builder.HasOne(s => s.Client).WithMany(c => c.Sales).HasForeignKey(s => s.ClientId);
		}
	}
}
