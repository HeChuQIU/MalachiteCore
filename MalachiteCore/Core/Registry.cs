namespace MalachiteCore.Core;

public class Registry
{
    public Registry(string group)
    {
        Group = group;
    }

    public string Group { get; }
    public Dictionary<string, RegistrableObject> RegisteredData { get; } = new();

    public void RegistryData(RegistrableObject registrableData)
    {
        if (registrableData.Group != "" && registrableData.Group != Group)
            throw new Exception($"待注册的数据组 {registrableData.Group} 与注册器的数据组 {Group} 不匹配");
        RegisteredData.Add(registrableData.Id,registrableData);
    }
}