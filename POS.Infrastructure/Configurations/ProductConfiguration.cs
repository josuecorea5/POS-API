using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;
using POS.Domain.Enums;

namespace POS.Infrastructure.Configurations
{
	public class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.Property(p => p.Name)
				.IsRequired()
				.HasMaxLength(100);
			builder.Property(p => p.UnitPrice)
				.IsRequired()
				.HasPrecision(16, 2);
			builder.Property(p => p.Status).HasDefaultValue(StatusEnum.Active);
		}
	}
}
