using Framework.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vehicle.Domain;
using Vehicle.Domain.PropertyTypes;

namespace Vehicle.Infra.Persistance.EF.Mapper
{
    public class PropertyTypeMapper : IEntityTypeConfiguration<PropertyType>
    {
        public void Configure(EntityTypeBuilder<PropertyType> builder)
        {
            builder.HasKey(x => x.Id).HasName("PropertyTypeId");
            builder.HasDiscriminator<PropertyTypeEnum>("PropertyType")
             .HasValue<TextType>(PropertyTypeEnum.Text)
             .HasValue<SelectiveType>(PropertyTypeEnum.Selective)
            .HasValue<NumericType>(PropertyTypeEnum.Numeric);
        }
    }
}
