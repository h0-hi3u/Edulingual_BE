using System.ComponentModel;

namespace Edulingual.Common.Helper;


public static class EnumHelper
{ 
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
