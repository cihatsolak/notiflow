namespace Puzzle.Lib.Assistant.Enums
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class EnumAttribute : Attribute
    {
        public string Name { get; init; }

        public EnumAttribute(string name)
        {
            Name = name;
        }
    }
}
