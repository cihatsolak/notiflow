namespace Notiflow.Panel.Models;

public record Response
{
    public virtual bool IsSuccess => StatusCode >= StatusCodes.Status200OK && StatusCode <= StatusCodes.Status226IMUsed;
    public bool IsFailure => !IsSuccess;
    public int StatusCode { get; set; } 

    [JsonRequired]
    public string Message { get; set; }
}

public sealed record Response<TData> : Response
{
    public TData Data { get; init; }
}
