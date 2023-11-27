namespace Puzzle.Lib.Localize.Infrastructure;

public sealed record LocalizeSetting
{
    public string ResourcesPath { get; set; }
    public string SharedDataAnnotationBaseName { get; set; }
    public string SharedDataAnnotationLocation { get; set; }
}
