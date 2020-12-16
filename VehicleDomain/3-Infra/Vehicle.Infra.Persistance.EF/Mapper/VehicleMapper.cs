using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vehicle.Domain;
using Vehicle.Domain.Vehicles;

namespace Vehicle.Infra.Persistance.EF.Mapper
{
    public class VehicleDomainMapper : IEntityTypeConfiguration<VehicleDomain>
    {
        public void Configure(EntityTypeBuilder<VehicleDomain> builder)
        {
            builder.ToTable("Vehicle");
            builder.HasKey("id").HasName("VehicleId");
            
            
        }
    }
}
