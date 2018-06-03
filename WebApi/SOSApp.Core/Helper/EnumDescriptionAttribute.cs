using System;
using System.ComponentModel;
using System.Reflection;

namespace WhiteRaven.Core.Helper
{
    public class EnumDescriptionAttribute : Attribute
    {
        public string StringValue { get; protected set; }
        public EnumDescriptionAttribute(string value)
        {
            this.StringValue = value;
        }

        public static T GetValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                if (GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException("Not found.", "description");
            // or return default(T);
        }

        public static string GetDescriptionFromValue<T>(T Value)
        {
            FieldInfo info = Value.GetType().GetField(Value.ToString());

            EnumDescriptionAttribute[] attributes = (EnumDescriptionAttribute[])info.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].StringValue;
            else
                return Value.ToString();
        }

        public static bool CheckValidValue<T>(int value)
        {
            if (System.Enum.IsDefined(typeof(T), value))
                return true;
            else
                return false;
        }
    }
}
