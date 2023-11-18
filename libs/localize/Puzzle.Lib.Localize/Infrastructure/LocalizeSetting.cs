namespace Puzzle.Lib.Localize.Infrastructure;

public sealed record LocalizeSetting
{
    public string ResourcesPath { get; set; }
    public Type ResourcesSource { get; set; }
}
