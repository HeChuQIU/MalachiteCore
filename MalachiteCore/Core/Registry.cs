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
            throw new Exception($"��ע��������� {registrableData.Group} ��ע������������ {Group} ��ƥ��");
        RegisteredData.Add(registrableData.Id,registrableData);
    }
}