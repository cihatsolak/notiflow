namespace Notiflow.Lib.Auth.Models
{
    public sealed record TokenResponseModel
    {
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }
        public int ExpiresIn => Convert.ToInt32((AccessTokenExpiration - DateTime.Now.AddMinutes(1)).TotalSeconds);

        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
