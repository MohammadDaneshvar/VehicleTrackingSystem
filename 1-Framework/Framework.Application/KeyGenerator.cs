using System.Linq;

namespace Framework.Application
{
    public class KeyGenerator : IKeyGenerator
    {
        
            public string GenerateKeyForCache(IRestrictedCommand command)
        {
            var attr =
                command.GetType().GetCustomAttributes(typeof(CacheOutputAttribute), true)
                    .FirstOrDefault() as CacheOutputAttribute;
            if (attr == null) return null;
            var key = command.GetType().FullName;
            foreach (var info in command.GetType().GetProperties())
            {
                var cparam = info.GetCustomAttributes(typeof(CacheParameterAttribute), true)
                    .FirstOrDefault() as CacheParameterAttribute;
                if (cparam != null)
                    key += "," + info.GetValue(command, new object[0]);
            }
            return key;
        }
        public string GenerateKeyForPKI(IRestrictedCommand command)
        {
            var attr =
                command.GetType().GetCustomAttributes(typeof(PKIValidationAttribute), true)
                    .FirstOrDefault() as PKIParameterAttribute;
            if (attr == null) return null;
            var key = command.GetType().FullName;
            foreach (var info in command.GetType().GetProperties())
            {
                var cparam = info.GetCustomAttributes(typeof(PKIParameterAttribute), true)
                    .FirstOrDefault() as CacheParameterAttribute;
                if (cparam != null)
                    key += "," + info.GetValue(command, new object[0]);
            }
            return key;
        }
    }
}