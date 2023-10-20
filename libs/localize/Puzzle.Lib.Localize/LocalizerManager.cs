namespace Puzzle.Lib.Localize;

internal sealed class LocalizerManager<TResource> : ILocalizerService<TResource> where TResource : class, new()
{
    private readonly IStringLocalizer _localizer;

    public LocalizerManager(IStringLocalizerFactory stringLocalizerFactory)
    {
        _localizer = stringLocalizerFactory.Create(typeof(TResource));
    }

    public string this[object name] => _localizer[$"{name}"];
}
