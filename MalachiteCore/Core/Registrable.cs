using System.Reflection;

namespace MalachiteCore.Core;

public abstract class Registrable
{
    public Dictionary<string, Type> PropertiesType { get; } = new();
    protected Dictionary<string, PropertyInfo> PropertyInfos { get; } = new();

    public bool ContainsPropertyName(string propertiesName) => PropertyInfos.ContainsKey(propertiesName);

    public object? GetPropertyValue(string propertyName)
    {
        return PropertyInfos[propertyName]?.GetMethod?.Invoke(this, null);
    }

    protected Registrable()
    {
        foreach (var propertyInfo in GetType().GetProperties())
        {
            PropertiesType.Add(propertyInfo.Name, propertyInfo.PropertyType);
            PropertyInfos.Add(propertyInfo.Name,propertyInfo);
        }
    }
}