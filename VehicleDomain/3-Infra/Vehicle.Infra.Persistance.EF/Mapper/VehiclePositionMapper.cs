using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vehicle.Domain.VehiclePositions;

namespace Vehicle.Infra.Persistance.EF.Mapper
{
    public class VehiclePositionMapper : IEntityTypeConfiguration<VehiclePosition>
    {
        public void Configure(EntityTypeBuilder<VehiclePosition> builder)
        {
            builder.HasKey("id").HasName("VehiclePositionId");
            //builder.HasMany<PropertyValue>()
            //   .WithOne().HasForeignKey(x=>x.VehiclePositionId);
        }
    }
}
