namespace Puzzle.Lib.Localize;

public interface ILocalizerService<TResource> where TResource : class, new()
{
    string this[object name] { get; }
}

internal sealed class LocalizerManager<TResource> : ILocalizerService<TResource> where TResource : class, new()
{
    private readonly IStringLocalizer _localizer;

    public LocalizerManager(IStringLocalizerFactory stringLocalizerFactory)
    {
        _localizer = stringLocalizerFactory.Create(typeof(TResource));
    }

    public string this[object name] => _localizer[$"{name}"];
}
