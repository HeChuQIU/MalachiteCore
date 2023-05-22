namespace MalachiteCore.Core;

public class Registry
{
    public Registry(string group)
    {
        Group = group;
    }

    public string Group { get; }
    public Dictionary<string, IRegistrableData> RegistrableDatas { get; } = new();

    public void RegistryData(IRegistrableData registrableData)
    {
        if (registrableData.Group != "" && registrableData.Group != Group)
            throw new Exception($"待注册的数据组 {registrableData.Group} 与注册器的数据组 {Group} 不匹配");
        RegistrableDatas.Add(registrableData.Id,(IRegistrableData)registrableData.Clone());
    }
}