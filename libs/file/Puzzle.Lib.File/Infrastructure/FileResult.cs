namespace Puzzle.Lib.File.Infrastructure;

public sealed record FileResult
{
    public bool Succeeded { get; init; }
    public string Url { get; init; }

    public static FileResult Success(string url)
    {
        return new FileResult()
        {
            Url = url,
            Succeeded = true
        };
    }

    public static FileResult Fail()
    {
        return new FileResult();
    }

    public static FileResult Return(bool succeeded, string url)
    {
        if (!succeeded)
        {
            return Fail();
        }

        return Success(url);
    }
}
