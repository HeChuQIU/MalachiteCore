namespace MalachiteCore.Core;

public class Malachite
{
    public static Malachite Instance { get; } = new();

    private readonly Dictionary<string, Dictionary<string, RegistrableObject>> _registeredData = new();

    private Malachite()
    {
    }

    public RegistrableObject GetRegistrableData(RegistrationId fullId)
    {
        if (!_registeredData.TryGetValue(fullId.Group, out var groupDict))
            throw new Exception($"�� {fullId.Group} δע��");
        if (!groupDict.TryGetValue(fullId.Id, out var data))
            throw new Exception($"Id {fullId.Group}:{fullId.Id} δע��");
        return data;
    }

    public void Registry(Registry registry)
    {
        if (!_registeredData.TryGetValue(registry.Group, out var groupDict))
        {
            groupDict = new();
            _registeredData.Add(registry.Group, groupDict);
        }

        foreach (var (id, data) in registry.RegisteredData)
        {
            if (groupDict.ContainsKey(id))
                throw new Exception($"Id {registry.Group}:{id} ��ע��");
            groupDict.Add(id, data);
        }
    }
}