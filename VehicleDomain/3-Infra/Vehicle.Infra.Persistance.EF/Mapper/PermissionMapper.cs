using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vehicle.Domain;

namespace Vehicle.Infra.Persistance.EF.Mapper
{
    public class PermissionMapper : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permission");

            builder.HasKey("id").HasName("PermissionId");
            
            builder.Property("id").ValueGeneratedNever();
            
            builder.Property(e => e.Name)
                  .IsRequired()
                  .HasMaxLength(100);

            builder.Property(e => e.Description)
                 .IsRequired()
                 .HasMaxLength(200);
        }
    }
}
