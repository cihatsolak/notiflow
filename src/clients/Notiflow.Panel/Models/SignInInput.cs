namespace Notiflow.Panel.Models.Auths;

public sealed class SignInInput
{
    [DataType(DataType.Text)]
    [DisplayName("Kullanıcı Adı")]
    public string Username { get; set; }

    [DataType(DataType.Password)]
    [DisplayName("Şifre")]
    public string Password { get; set; }

    [DisplayName("Beni Hatırla")]
    public bool RememberMe { get; set; }
}
