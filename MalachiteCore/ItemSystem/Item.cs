using MalachiteCore.Core;

namespace MalachiteCore.ItemSystem;

public abstract class Item : Registrable
{
    public ItemProperties? Properties { get; protected set; }
}