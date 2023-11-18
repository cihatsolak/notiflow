var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCookieAuthentication();

var authorizationPolicy = new AuthorizationPolicyBuilder(CookieAuthenticationDefaults.AuthenticationScheme)
                            .RequireAuthenticatedUser()
                            .RequireClaim(ClaimTypes.NameIdentifier)
                            .RequireClaim(ClaimTypes.Name)
                            .RequireClaim(ClaimTypes.Surname)
                            .RequireClaim(ClaimTypes.NameIdentifier)
                            .RequireClaim(ClaimTypes.Email)
                            .Build();

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AuthorizeFilter(authorizationPolicy));
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
})
.AddRazorRuntimeCompilation();

builder.Services.AddMultiLanguage();

builder.Services.AddHttpContextAccessor();
builder.Services.TryAddSingleton<IAuthService, AuthManager>();

builder.Services.AddHttpServices();

builder.Services.AddTagHelperInitializers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

app.UseCookiePolicy(new CookiePolicyOptions
{
    Secure = CookieSecurePolicy.Always,
    HttpOnly = HttpOnlyPolicy.Always,
    MinimumSameSitePolicy = SameSiteMode.Strict
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authentication}/{action=SignIn}/{id?}");

await app.StartProjectAsync();
