namespace MalachiteCore.Core;

public record RegistrationId(string Id, string Group)
{
    public override string ToString()
    {
        return $"{Group}:{Id}";
    }
}