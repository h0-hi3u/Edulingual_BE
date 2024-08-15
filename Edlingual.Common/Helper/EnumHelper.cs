using System.ComponentModel;

namespace Edulingual.Common.Helper;


public static class EnumHelper
{
    public static T ParseEnum<T>(string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }

    public static string GetDescriptionFromEnum<T>(this T value)
    {
        if (value == null) return string.Empty;
        var fi = value?.GetType().GetField(value.ToString()!);
        if (fi != null)
        {
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
        }

        return value.ToString()!;
    }

    public static IEnumerable<T> GetValues<T>()
    {
        return Enum.GetValues(typeof(T)).Cast<T>();
    }
    public static T GetEnumValueFromString<T>(string valueName) where T : struct, Enum
    {
        var values = Enum.GetValues<T>();

        foreach (var value in values)
        {
            if (Equals(value.ToString(), valueName))
            {
                return value;
            }
        }

        throw new Exception($"Enum does not contain value {valueName}");
    }
}
