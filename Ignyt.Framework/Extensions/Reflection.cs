using System;
using System.Reflection;
using System.Linq;

/// <summary>
/// 	Extension methods for the array data type
/// </summary>
public static class Reflection {
    public static TValue GetAttributValue<TAttribute, TValue>(this PropertyInfo prop, Func<TAttribute, TValue> value) where TAttribute : Attribute {
        if (prop.GetCustomAttributes(
            typeof(TAttribute), true
            ).FirstOrDefault() is TAttribute att) {
            return value(att);
        }
        return default(TValue);
    }
}
