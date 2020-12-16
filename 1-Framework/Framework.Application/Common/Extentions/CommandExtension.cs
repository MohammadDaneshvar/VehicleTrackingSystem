using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Framework.Application.Common.Extentions
{
    public static class CommandExtension
    {
        public static object GetResult(this IRestrictedCommand command)
        {
            var resultType = command.GetType().GetInterfaces().Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IHaveResult<>)).FirstOrDefault().GetGenericArguments()[0];
            var resultPropertyName = typeof(IHaveResult<>).MakeGenericType(resultType).GetProperties().Where(x => x.PropertyType == resultType).FirstOrDefault().Name;
            var commandResult = command.GetType().GetProperty(resultPropertyName).GetValue(command);
            return commandResult;
        }
        public static bool HaveResult(this IRestrictedCommand command)
        {
            bool haveResult = command.GetType().GetInterfaces()
                   .Where(i => i.IsGenericType)
                   .Any(i => i.GetGenericTypeDefinition() == typeof(IHaveResult<>));
            return haveResult;
        }
    }
}
