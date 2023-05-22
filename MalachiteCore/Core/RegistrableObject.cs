namespace MalachiteCore.Core;

public abstract class RegistrableObject : DynamicObject
{
    public string Id { get; protected set; }
    public string Group { get; protected set; }

    protected RegistrableObject(string id, string group = "")
    {
        Id = id;
        Group = group;
    }

//TODO
}