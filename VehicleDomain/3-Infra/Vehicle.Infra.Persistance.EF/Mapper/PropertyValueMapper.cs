using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vehicle.Domain.Permissions;
using Vehicle.Domain.VehiclePositions;
using Vehicle.Domain.VehicleProperties;

namespace Vehicle.Infra.Persistance.EF.Mapper
{
    public class PropertyValueMapper : IEntityTypeConfiguration<PropertyValue>
    {
        public void Configure(EntityTypeBuilder<PropertyValue> builder)
        {
            builder.HasKey(x => new { x.VehiclePositionId, x.PropertyId });
            builder.Property(x => x.Value);

            builder.HasOne<VehiclePosition>(x=>x.VehiclePosition)
                .WithMany(x=>x.propertyValues).HasForeignKey(x => x.VehiclePositionId);


            builder.HasOne<Property>(x=>x.Property)
                .WithMany(x => x.PropertyValues).HasForeignKey(x => x.PropertyId);
            




        }
    }
}
