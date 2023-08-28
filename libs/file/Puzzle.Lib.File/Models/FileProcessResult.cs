namespace Puzzle.Lib.File.Models;

public sealed record FileProcessResult
{
    public bool Succeeded { get; init; }
    public string Url { get; init; }

    public static FileProcessResult Success(string url)
    {
        return new FileProcessResult()
        {
            Url = url,
            Succeeded = true
        };
    }

    public static FileProcessResult Fail()
    {
        return new FileProcessResult();
    }

    public static FileProcessResult Return(bool succeeded, string url)
    {
        if (!succeeded)
        {
            return Fail();
        }

        return Success(url);
    }
}
