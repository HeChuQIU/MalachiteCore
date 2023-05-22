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
            throw new Exception($"��ע��������� {registrableData.Group} ��ע������������ {Group} ��ƥ��");
        RegistrableDatas.Add(registrableData.Id,(IRegistrableData)registrableData.Clone());
    }
}