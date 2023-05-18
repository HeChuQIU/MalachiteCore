namespace MalachiteCore.ItemSystem;

public class ItemCreator
{
    private ItemCreator() { }

    private static readonly ItemCreator ic = new ItemCreator();

    public static ItemCreator GetInstance() => ic;

    public void SetId(string id) { }

    public void SetName(string name) { }
}