namespace Puzzle.Lib.Logging.Infrastructure;

public sealed record SeriLogElasticSetting
{
    [Required]
    public string Address { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public bool IsRequiredAuthentication { get; set; }
}
