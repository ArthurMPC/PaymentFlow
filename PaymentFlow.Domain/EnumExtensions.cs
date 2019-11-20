using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PaymentFlow.Domain
{
    public static class EnumExtensions
    {
        public static string GetDescription<TEnum>(this TEnum item)
            => item.GetType()
                .GetField(item.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .Cast<DescriptionAttribute>()
                .FirstOrDefault()?.Description ?? string.Empty;

        public static int GetValue<TEnum>(this TEnum item)
            => Convert.ToInt32(item);

        public static T GetValueFromDescription<T>(string description)
        {
            var value = default(T);
            var type = typeof(T);

            if (!type.IsEnum) throw new InvalidOperationException();

            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        value = (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        value = (T)field.GetValue(null);
                }
            }

            return value;
        }
    }
}
