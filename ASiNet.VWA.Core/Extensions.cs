namespace ASiNet.VWA.Core;
public static class Extensions
{
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (var element in source)
            action(element);
    }

    public static bool ContainsProperty(this object obj, string propertyName) =>
        obj.GetType().GetProperty(propertyName) is not null;

    public static (object obj, Type type)? ContainsPropertyTo(this object? obj, string propertyName, Action<object, string> execute)
    {
        if(obj is null)
            return null;
        var type = obj.GetType();
        if (obj.GetType().GetProperty(propertyName) is not null)
            execute?.Invoke(obj, propertyName);
        return (obj, type);
    }

    public static (object obj, Type type)? ContainsPropertyTo(this (object obj, Type type)? oAndT, string propertyName, Action<object, string> execute)
    {
        if (oAndT is null)
            return null;
        if (oAndT.Value.type.GetProperty(propertyName) is not null)
            execute?.Invoke(oAndT.Value.obj, propertyName);
        return oAndT;
    }
}
