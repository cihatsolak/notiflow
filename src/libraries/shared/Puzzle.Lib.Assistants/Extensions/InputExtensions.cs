namespace Puzzle.Lib.Assistants.Extensions
{
    public static class InputExtensions
    {
        public static string ToClearUnnecessaryCharactersInNumber(this string number)
        {
            return number.Replace("(", string.Empty).Replace(")", string.Empty).Replace("-", string.Empty).Replace(" ", string.Empty);
        }
    }
}
