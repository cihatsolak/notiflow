var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCookieAuthentication();

var authorizationPolicy = new AuthorizationPolicyBuilder()
                            .RequireAuthenticatedUser()
                            .RequireClaim(ClaimTypes.NameIdentifier)
                            .RequireClaim(ClaimTypes.Email)
                            .Build();

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AuthorizeFilter(authorizationPolicy));
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
})
.AddRazorRuntimeCompilation();

builder.Services.AddHttpContextAccessor();
builder.Services.TryAddSingleton<IAuthService, AuthManager>();

builder.Services.AddHttpServices();

builder.Services.AddTagHelperInitializers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

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
