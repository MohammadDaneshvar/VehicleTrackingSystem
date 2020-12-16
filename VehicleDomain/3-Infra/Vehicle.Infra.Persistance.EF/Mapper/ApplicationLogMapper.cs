using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vehicle.Domain.Logs;

namespace Vehicle.Infra.Persistance.EF.Mapper
{

    public class ApplicationLogMapper : IEntityTypeConfiguration<ApplicationLog>
    {
        public void Configure(EntityTypeBuilder<ApplicationLog> builder)
        {
            builder.ToTable("ApplicationLog");

            builder.Property(e => e.Application)
                    .IsRequired()
                    .HasMaxLength(50);

            builder.Property(e => e.Ip)
                    .HasColumnName("IP")
                    .HasMaxLength(50);

            builder.Property(e => e.Level)
                    .IsRequired()
                    .HasMaxLength(50);

            builder.Property(e => e.Logged).HasColumnType("datetime");

            builder.Property(e => e.Logger).HasMaxLength(250);

            builder.Property(e => e.MachineName)
                    .IsRequired()
                    .HasMaxLength(50);

            builder.Property(e => e.Message).IsRequired();

            builder.Property(e => e.UserName).HasMaxLength(50);
        }
    }
}