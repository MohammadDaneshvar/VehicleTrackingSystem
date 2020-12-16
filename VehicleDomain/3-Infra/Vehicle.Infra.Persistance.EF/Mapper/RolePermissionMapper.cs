using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vehicle.Domain;

namespace Vehicle.Infra.Persistance.EF.Mapper
{
    public class RolePermissionMapper : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.ToTable("RolePermission");
            builder.HasKey("id");
            //builder.HasOne(d => d.Permission)
            //        .WithMany(p => p.RolePermissions)
            //        .HasForeignKey(d => d.PermissionId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_Permission_RolePermission");
        }
    }
}
