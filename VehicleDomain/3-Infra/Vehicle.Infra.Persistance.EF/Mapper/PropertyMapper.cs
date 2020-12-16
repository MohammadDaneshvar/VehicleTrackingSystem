using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vehicle.Domain.Permissions;
using Vehicle.Domain.VehiclePositions;
using Vehicle.Domain.VehicleProperties;

namespace Vehicle.Infra.Persistance.EF.Mapper
{
    public class PropertyMapper : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.HasKey(x=>x.Id).HasName("PropertyId"); ;
            builder.Property(x => x.Id).ValueGeneratedNever();
            

            builder.Property(e => e.Name)
                  .IsRequired()
                  .HasMaxLength(100);

            //builder.HasMany<PropertyValue>("PropertyValue")
            //    .WithOne("Property").HasForeignKey(x => x.PropertyId);


        }
    }
}
