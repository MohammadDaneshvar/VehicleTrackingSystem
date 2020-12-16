using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Validation
{
    public static class ValidatiionExtention
    {
        public static void MustBeNotNull(this object obj, string message = "object can't be null")
        {
            if (obj == null)
                throw new ArgumentNullException(message);
        }
        public static void MustBeNotEmpty(this IEnumerable list, string message = "list can't be null or empty")
        {
            if (list != null)
            {
                var it = list.GetEnumerator();
                if (it.MoveNext()) return;
            }
            throw new ArgumentNullException(message);
        }
    }
}
