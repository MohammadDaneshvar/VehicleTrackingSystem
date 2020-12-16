using Framework.Application;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;

namespace App.Service.AspDotNetDistributor.Filters
{
    public static class HttpEncode
    {
        private static ILogger _logger = LogManager.GetCurrentClassLogger();

        public static void ParseProperties(this object model)
        {
            if (model == null) return;

            var json = JsonConvert.SerializeObject(model);
            _logger.Info(json);

            if (IsPropertyArrayOrList(model.GetType()))
            {
                ParsePropertiesOfList(model);
            }
            else
            {
                GetAllProperties(model).ForEach(t => EncodeField(t, model));
            }
        }

        private static void ParsePropertiesOfList(object model)
        {
            foreach (var item in (IEnumerable)model)
            {
                ParseProperties(item);
            }
        }

        private static List<PropertyInfo> GetAllProperties(object value) => value?.GetType()?.GetProperties()?.ToList();

        private static void EncodeField(PropertyInfo p, object arg)
        {
            try
            {
                if (p.GetIndexParameters().Length != 0 || p.GetValue(arg) == null)
                    return;

                if (IsUserDefinedClass(p.PropertyType) && p.CanWrite)
                {
                    ParseProperties(p.GetValue(arg));
                }
                else if (IsPropertyArrayOrList(p.PropertyType) && p.CanWrite)
                {
                    ParseArrayOrListProperty(p, arg);
                }
                else if (p.PropertyType == typeof(string) && p.CanWrite)
                {
                    var encodedValue = HtmlEncode(p.GetValue(arg)?.ToString());
                    SetPropertyValue(p, arg, encodedValue);
                }
            }
            catch (Exception)
            {

            }
        }

        private static void ParseArrayOrListProperty(PropertyInfo p, object arg)
        {
            if (p.GetValue(arg) is string[] || p.GetValue(arg) is List<string>)
            {
                SetPropertyValueOfStaringArrayType(p, arg);
            }
            else
            {
                ParsePropertiesOfList(p.GetValue(arg));
            }
        }

        private static void SetPropertyValueOfStaringArrayType(PropertyInfo propertyInfo, object arg)
        {
            if (propertyInfo.GetValue(arg) is string[] stringValue)
            {
                var result = new List<string>();
                stringValue.ToList().ForEach(l => result.Add(HtmlEncode(l)));
                SetPropertyValue(propertyInfo, arg, result.Any() ? result.ToArray() : null);
            }
            else if (propertyInfo.GetValue(arg) is List<string> listValue)
            {
                var result = new List<string>();
                listValue.ForEach(l => result.Add(HtmlEncode(l)));
                SetPropertyValue(propertyInfo, arg, result.Any() ? result : null);
            }
        }

        private static bool IsUserDefinedClass(Type type) =>
            type.IsClass &&
            !type.FullName.StartsWith("System.");

        private static bool IsPropertyArrayOrList(Type type) =>
            type.IsArray && type.GetElementType() == typeof(string) ||
            (type != typeof(string) && type.GetInterface(typeof(IEnumerable<>).FullName) != null);

        private static void SetPropertyValue(PropertyInfo propertyInfo, object allValue, object value)
        {
            propertyInfo.SetValue(allValue, value);
        }

        private static string HtmlEncode(string value) => HttpUtility.HtmlEncode(value);
    }
}
