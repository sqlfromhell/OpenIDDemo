var builder = WebApplication
    .CreateBuilder(args);

var settings = builder.Configuration
    .GetSection("Token")
    .Get<TokenSettings>();

builder.Services
    .AddSingleton(settings)
    .AddAuthentication
        (CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Account/Login";
        option.LogoutPath = "/Account/Logout";
        //option.ReturnUrlParameter = "returnUrl";
    })
    .AddJwtBearer(options =>
        options.TokenValidationParameters
            = settings.GetParameters());

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