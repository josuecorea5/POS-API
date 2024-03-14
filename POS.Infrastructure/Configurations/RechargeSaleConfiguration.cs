using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;
using POS.Domain.Enums;

namespace POS.Infrastructure.Configurations
{
	public class RechargeSaleConfiguration : IEntityTypeConfiguration<RechargeSale>
	{
		public void Configure(EntityTypeBuilder<RechargeSale> builder)
		{
			builder.Property(rs => rs.SaleId).IsRequired();
			builder.Property(rs => rs.Percentage)
				.IsRequired()
				.HasPrecision(4,2);
			builder.Property(rs => rs.NewTotal)
				.IsRequired()
				.HasPrecision(16, 2);
			builder.Property(rs => rs.RechargeSaleStatus)
				.HasDefaultValue(RechargeSaleStatus.PENDING);
			builder.Property(rs => rs.Status).HasDefaultValue(StatusEnum.Active);
			builder.Property(rs => rs.Description).IsRequired().HasMaxLength(200);
			builder.Property(rs => rs.LimitDate).IsRequired();
			builder.HasOne(rs => rs.Sale).WithMany(s => s.RechargeSales).HasForeignKey(rs => rs.SaleId);
		}
	}
}
