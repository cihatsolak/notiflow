namespace Puzzle.Lib.Localize;

public interface ILocalizerService<TResource> where TResource : class, new()
{
    string this[object name] { get; }
}
