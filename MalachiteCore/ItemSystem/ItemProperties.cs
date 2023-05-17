namespace MalachiteCore.ItemSystem;

public abstract class ItemProperties
{
    public string? Id { get; protected set; }
    public string? Name { get; protected set; }
    public string? Description { get; protected set; }
    public int MaxStackSize { get; protected set; }
}