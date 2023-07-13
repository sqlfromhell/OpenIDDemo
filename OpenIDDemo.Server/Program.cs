var builder = WebApplication
    .CreateBuilder(args);

var settings = builder.Configuration
    .GetSection("Token")
    .Get<TokenSettings>();

builder.Services
    .AddSingleton(settings)
    .AddScoped<IUserRepository, UserRepository>();

builder.Services
    .AddAuthentication
        (CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Account/Login";
        option.LogoutPath = "/Account/Logout";
    })
    .AddJwtBearer(options =>
        options.TokenValidationParameters
            = settings.GetParameters());

builder.Services
    .AddControllersWithViews();

var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();