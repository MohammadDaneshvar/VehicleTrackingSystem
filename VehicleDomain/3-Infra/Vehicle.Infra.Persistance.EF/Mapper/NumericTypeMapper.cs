using Framework.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vehicle.Domain;
using Vehicle.Domain.PropertyTypes;

namespace Vehicle.Infra.Persistance.EF.Mapper
{
    public class NumericTypeMapper : IEntityTypeConfiguration<NumericType>
    {
        public void Configure(EntityTypeBuilder<NumericType> builder)
        {
            builder.Property(x=>x.Min).HasColumnName("Min");
            builder.Property(x => x.Max).HasColumnName("Max");

        }
    }
}
