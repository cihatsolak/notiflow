namespace Puzzle.Lib.Assistant.Enums;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
public class EnumAttribute : Attribute
{
    public required string Name { get; init; }

    public EnumAttribute(string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);

        Name = name;
    }
}
