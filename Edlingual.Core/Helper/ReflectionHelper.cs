namespace EduLingual.Common.Helper;


public static class ReflectionHelper
{
    public static IEnumerable<Type> GetAssignableTo(Type type)
    {
        var asms = AppDomain.CurrentDomain.GetAssemblies();

        var types = asms.SelectMany(asm => asm.GetTypes()).ToList();

        var result = types.Where(t => t.IsClass && !t.IsAbstract && t.IsAssignableTo(type));

        return result;
    }
    public static dynamic CheckType(object? obj, Type T)
    {
        if (obj == null)
        {
            return null;
        }
        else
        {
            return Convert.ChangeType(obj, T);
        }
    }
}
