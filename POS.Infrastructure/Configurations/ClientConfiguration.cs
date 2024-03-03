using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;
using POS.Domain.Enums;

namespace POS.Infrastructure.Configurations
{
	public class ClientConfiguration : IEntityTypeConfiguration<Client>
	{
		public void Configure(EntityTypeBuilder<Client> builder)
		{
			builder.Property(c => c.FullName)
				.IsRequired()
				.HasMaxLength(100);
			builder.Property(c => c.Status).HasDefaultValue(StatusEnum.Active);
		}
	}
}
