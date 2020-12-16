using System.Threading.Tasks;

namespace Vehicle.Domain.VehicleProperties
{
    public interface IPropertyValidator
    {
        bool PropertyIsDuplication(Property property);
    }
}
