using System.Linq;
using System.Reflection;

namespace Framework.Domain.Aggregate
{
    public class ValueObject : IValueObject
    {
        protected ValueObject()
        {
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;
            return GetType().GetFields(BindingFlags.NonPublic |
                                       BindingFlags.Instance)
                .All(p => p.GetValue(this).Equals(p.GetValue(obj)));
        }

        public override int GetHashCode()
        {
            int hashCode = 31;
            bool changeMultiplier = false;
            const int index = 1;

            // Compare all public properties
            PropertyInfo[] publicProperties = GetType().GetProperties();

            if (publicProperties.Any())
            {
                foreach (object value in publicProperties
                    .Select(item => item.GetValue(this, null)))
                {
                    if (value != null)
                    {
                        hashCode = hashCode*((changeMultiplier) ? 59 : 114)
                                   + value.GetHashCode();
                        changeMultiplier = !changeMultiplier;
                    }
                    else
                        hashCode = hashCode ^ (index*13); // Support order
                }
            }

            return hashCode;
        }
    }
}