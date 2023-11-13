namespace Notiflow.Panel.Controllers;

public sealed class LanguageController : Controller
{
    [HttpPost]
    public IActionResult Change(string culture, string returnUrl)
    {
        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = D                                                                                                                                                                                                             ateTimeOffset.UtcNow.AddYears(1) }
        );

        return LocalRedirect(returnUrl);
    }
}
