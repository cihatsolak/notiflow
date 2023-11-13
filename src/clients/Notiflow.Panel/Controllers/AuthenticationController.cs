﻿namespace Notiflow.Panel.Controllers;

[AllowAnonymous]
public sealed class AuthenticationController : Controller
{
    private readonly IAuthService _authService;
    private readonly IStringLocalizer<AuthenticationController> _localizer;

    public AuthenticationController(
        IAuthService authService, 
        IStringLocalizer<AuthenticationController> localizer)
    {
        _authService = authService;
        _localizer = localizer;
    }

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
            ModelState.AddModelError(string.Empty, _localizer["login.error.message"]);
            return View(signInInput);
        }

        bool succeeded = await _authService.SignInAsync(signInInput, cancellationToken);
        if (!succeeded)
        {
            ModelState.AddModelError(string.Empty, _localizer["login.error.message"]);
            return View(signInInput);
        }

        string redirectUrl = TempData["RedirectUrl"] as string;
        if (string.IsNullOrWhiteSpace(redirectUrl))
            return RedirectToAction(nameof(HomeController.Index), "Home");

        if (!Url.IsLocalUrl(redirectUrl))
            return Redirect("/");

        return Redirect(redirectUrl);
    }
}
