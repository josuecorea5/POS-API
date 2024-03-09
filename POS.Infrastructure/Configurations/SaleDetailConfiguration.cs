using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;
using POS.Domain.Enums;

namespace POS.Infrastructure.Configurations
{
	public class SaleDetailConfiguration : IEntityTypeConfiguration<SaleDetail>
	{
		public void Configure(EntityTypeBuilder<SaleDetail> builder)
		{
			builder.Property(sd => sd.SaleId)
				.IsRequired();
			builder.Property(sd => sd.ProductId) 
				.IsRequired();
			builder.Property(sd => sd.Quantity)
				.IsRequired();
			builder.Property(sd => sd.UnitPrice)
				.IsRequired()
				.HasPrecision(16, 2);
			builder.Property(sd => sd.Discount)
				.HasPrecision(16, 2);
			builder.Property(sd => sd.Status)
				.HasDefaultValue(StatusEnum.Active);
			builder.HasOne(sd => sd.Sale).WithMany(s => s.SaleDetails).HasForeignKey(sd => sd.SaleId);
			builder.HasOne(sd => sd.Product).WithMany(p => p.SaleDetail).HasForeignKey(sd => sd.ProductId);
		}
	}
}
