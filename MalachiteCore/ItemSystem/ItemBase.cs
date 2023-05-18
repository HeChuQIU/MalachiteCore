using MalachiteCore.Core;

namespace MalachiteCore.ItemSystem;

public abstract class ItemBase
{
    public string Id { get; protected set; }
    public HashSet<string> Tags { get; protected set; } = new();
    public DynamicObject? Properties { get; protected set; } = new();

    public ItemBase(string id)
    {
        this.Id = id;
    }

}