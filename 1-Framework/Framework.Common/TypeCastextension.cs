using System;
using System.Reflection;

namespace AppService.Common
{
    public static class TypeCastextension
    {
        public static object CastTo<T>(object value) where T : class
        {
            return value as T;
        }

        private static readonly MethodInfo CastToInfo = typeof(TypeCastextension).GetMethod("CastTo");

        public static object DynamicCast(object source, Type targetType)
        {
            return CastToInfo.MakeGenericMethod(new[] { targetType }).Invoke(null, new[] { source });
        }
    }

}