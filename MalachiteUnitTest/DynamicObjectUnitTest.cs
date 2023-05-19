using MalachiteCore.Core;

namespace MalachiteUnitTest;

public class DynamicObjectUnitTest
{
    [Theory]
    [InlineData("key", "value")]
    public void Add(string key, object value)
    {
        DynamicObject dynamicObject = new();
        Dictionary<string,object> dictionary = new();

        dynamicObject.Add(key, value);
        dictionary.Add(key, value);
        Assert.Equal(dictionary[key], dynamicObject[key]);
    }
}