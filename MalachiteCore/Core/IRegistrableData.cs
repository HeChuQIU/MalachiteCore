namespace MalachiteCore.Core;

public interface IRegistrableData : ICloneable
{
    public string Id { get; }
    public string Group { get; }
    public object this[string key] { get; }
}