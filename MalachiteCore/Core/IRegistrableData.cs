namespace MalachiteCore.Core;

//TODO: ʹ�ü̳еķ�ʽʹ RegistrableObject ���ɱ�
[Obsolete]
public interface IRegistrableData : ICloneable
{
    public string Id { get; }
    public string Group { get; }
    public object this[string key] { get; }
}