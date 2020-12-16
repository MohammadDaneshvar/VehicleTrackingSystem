using Framework.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vehicle.Domain;
using Vehicle.Domain.PropertyTypes;

namespace Vehicle.Infra.Persistance.EF.Mapper
{
    public class SelectiveTypeMapper : IEntityTypeConfiguration<SelectiveType>
    {
        public void Configure(EntityTypeBuilder<SelectiveType> builder)
        {
            builder.HasMany<Option>().WithOne().HasForeignKey("PropertyTypeId");
        }
    }
}
