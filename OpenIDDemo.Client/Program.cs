var builder = WebApplication
    .CreateBuilder(args);

var settings = builder.Configuration
    .GetSection("Token")
    .Get<TokenSettings>();

builder.Services
    .AddSingleton(settings);

builder.Services
    .AddAuthentication
        (CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(e =>
    {
        e.Cookie.Name =
            "Authentication";
        e.Cookie.HttpOnly = true;
        e.Cookie.SameSite =
            SameSiteMode.Strict;
        e.ExpireTimeSpan = TimeSpan
            .FromSeconds(settings.Expires);
        e.LoginPath = "/Account/Login";
        e.LogoutPath = "/Account/Logout";
        e.SlidingExpiration = true;
        e.ReturnUrlParameter = "returnUrl";
    });

builder.Services
    .AddControllersWithViews();

var app = builder.Build();

app.UseDeveloperExceptionPage()
    .UseStaticFiles()
    .UseRouting()
    .UseAuthentication()
    .UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();

namespace OpenIDDemo.Client
{
    public partial class Program
    { }
}