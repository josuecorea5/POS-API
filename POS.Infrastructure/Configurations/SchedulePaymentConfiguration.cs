using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;
using POS.Domain.Enums;

namespace POS.Infrastructure.Configurations
{
	public class SchedulePaymentConfiguration : IEntityTypeConfiguration<SchedulePayment>
	{
		public void Configure(EntityTypeBuilder<SchedulePayment> builder)
		{
			builder.Property(sp => sp.SaleId)
				.IsRequired();
			builder.Property(sp => sp.Amount)
				.IsRequired()
				.HasPrecision(16, 2);
			builder.Property(sp => sp.InitialAmount)
				.IsRequired()
				.HasPrecision(16, 2);
			builder.Property(sp => sp.CreatedDate)
				.IsRequired();
			builder.Property(sp => sp.LimitDate)
				.IsRequired();
			builder.Property(sp => sp.AmountRemaining)
				.IsRequired()
				.HasPrecision(16, 2);
			builder.Property(sp => sp.SchedulePaymentStatus)
				.HasDefaultValue(SchedulePaymentStatus.PENDING);
			builder.Property(sp => sp.Status)
				.HasDefaultValue(StatusEnum.Active);
			builder.HasOne(sp => sp.Sale).WithMany(s => s.SchedulePayments).HasForeignKey(sp => sp.SaleId);
		}
	}
}
