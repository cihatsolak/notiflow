namespace Notiflow.Panel.Controllers;

[AllowAnonymous]
public sealed class AuthenticationController(
    IAuthService authService,
    IStringLocalizer<AuthenticationController> localizer) : Controller
{

    [HttpGet]
    public IActionResult SignIn(string returnUrl, CancellationToken cancellationToken)
    {
        if (User.Identity.IsAuthenticated)
        {
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        if (!string.IsNullOrWhiteSpace(returnUrl))
        {
            TempData["RedirectUri"] = returnUrl;
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignIn(SignInInput signInInput, CancellationToken cancellationToken)
    {
        if (User.Identity.IsAuthenticated)
        {
            ModelState.AddModelError(string.Empty, localizer["login.error.message"]);
            return View(signInInput);
        }

        bool succeeded = await authService.SignInAsync(signInInput, cancellationToken);
        if (!succeeded)
        {
            ModelState.AddModelError(string.Empty, localizer["login.error.message"]);
            return View(signInInput);
        }

        string redirectUrl = TempData["RedirectUrl"] as string;
        if (string.IsNullOrWhiteSpace(redirectUrl))
            return RedirectToAction(nameof(HomeController.Index), "Home");

        if (!Url.IsLocalUrl(redirectUrl))
            return Redirect("/");

        return Redirect(redirectUrl);
    }

    [HttpPost]
    public async Task<IActionResult> SignOut(CancellationToken cancellationToken)
    {
        if (!User.Identity.IsAuthenticated)
            return RedirectToAction(nameof(SignIn));

        await authService.SignOutAsync(cancellationToken);

        return RedirectToAction(nameof(SignIn));
    }
}
