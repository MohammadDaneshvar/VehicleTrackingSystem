using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Framework.Application.Common.Extentions
{
    public static class EnumHelper
    {
        public static string GetDescription(this Enum value)
        {
            if (null == value || value.ToString() == "-1")
            {
                return string.Empty;
            }

            System.Reflection.FieldInfo field = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])field?.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes?.Length > 0 ? attributes[0]?.Description : value.ToString();
            
        }
        public static string GetDisplayName(Enum value)
        {
            DisplayAttribute displayAttribute = GetDisplayAttribute(value);
            return null == displayAttribute ? value.ToString() : displayAttribute.GetName();
        }

        private static DisplayAttribute GetDisplayAttribute<TEnum>(TEnum value)
        {
            return value.GetType()
                        .GetField(value.ToString())
                        .GetCustomAttributes(typeof(DisplayAttribute), false)
                        .Cast<DisplayAttribute>()
                        .FirstOrDefault();
        }
    }
}
