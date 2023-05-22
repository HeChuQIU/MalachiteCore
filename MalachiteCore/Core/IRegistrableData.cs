namespace MalachiteCore.Core;

//TODO: 使用继承的方式使 RegistrableObject 不可变
[Obsolete]
public interface IRegistrableData : ICloneable
{
    public string Id { get; }
    public string Group { get; }
    public object this[string key] { get; }
}