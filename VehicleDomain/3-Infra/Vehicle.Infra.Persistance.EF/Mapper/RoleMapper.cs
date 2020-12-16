using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vehicle.Domain.Permissions;

namespace Vehicle.Infra.Persistance.EF.Mapper
{
    public class RoleMapper : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey("id").HasName("RoleId");
            
            builder.Property(e => e.Name)
                  .IsRequired()
                  .HasMaxLength(100);

            
        }
    }
}
