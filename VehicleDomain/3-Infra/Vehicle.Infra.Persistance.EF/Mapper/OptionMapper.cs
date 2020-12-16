using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vehicle.Domain.PropertyTypes;

namespace Vehicle.Infra.Persistance.EF.Mapper
{
    public class OptionMapper : IEntityTypeConfiguration<Option>
    {
        public void Configure(EntityTypeBuilder<Option> builder)
        {
            builder.HasNoKey();
            builder.Property(x => x.Text).HasColumnName("Text");
            //   .WithMany().HasForeignKey(x => x.VehiclePositionId).HasPrincipalKey(x => x.VehicleId);


         



        }
    }
}
