namespace MalachiteCore.Core;

public class Malachite
{
    public static Malachite Instance { get; } = new();

    private Dictionary<string, Dictionary<string, RegistrableObject>> registredDatas = new();

    private Malachite()
    {
    }

    public IRegistrableData GetRegistrableData(RegistrationId fullId)
    {
        if (!registredDatas.TryGetValue(fullId.Group, out var groupDict))
            throw new Exception($"�� {fullId.Group} δע��");
        if (!groupDict.TryGetValue(fullId.Id, out var data))
            throw new Exception($"Id {fullId.Group}:{fullId.Id} δע��");
        return data;
    }

    public void Registry(Registry registry)
    {
        if (registredDatas.TryGetValue(registry.Group, out var groupDict))
            throw new Exception($"{registry.Group} ��ע��");

        groupDict = new();
        registredDatas.Add(registry.Group, groupDict);

        foreach (var (id, data) in registry.RegistrableDatas)
        {
            if (groupDict.ContainsKey(id))
                throw new Exception($"Id {registry.Group}:{id} ��ע��");
            groupDict.Add(id, (RegistrableObject)data.Clone());
        }
    }
}