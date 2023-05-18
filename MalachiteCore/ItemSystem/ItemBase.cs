using MalachiteCore.Core;

namespace MalachiteCore.ItemSystem;

public abstract class ItemBase
{
    public string Id { get; protected set; }
    public HashSet<string> Tags { get; protected set; } = new();
    public ItemEventBase BindEvents { get; protected set; }
    public DynamicObject Properties { get; protected set; } = new();

    public ItemBase(string id)
    {
        this.Id = id;
    }

    public ItemBase(string id, ItemEventBase bindEvents)
    {
        this.Id = id;
        this.BindEvents = bindEvents;
    }
}