using MalachiteCore.ItemSystem;

public class ItemTest : ItemBase
{
    public ItemTest(string id) : base(id)
    {

    }

    public void setProperties(string key, object value)
    {
        this.Properties.put(key, value);
    }

    public object getProperties(string key)
    {
        return this.Properties.get(key);
    }

}