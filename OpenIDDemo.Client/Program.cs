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
    .AddCookie(option =>
    {
        option.LoginPath = "/Account/Login";
        option.LogoutPath = "/Account/Logout";
    })
    .AddJwtBearer(options =>
        options.TokenValidationParameters
            = settings.GetParameters());

//.AddOpenIdConnect(options =>
//{
//    options.Authority = settings.Issuer;
//    options.ClientId = settings.Audience;
//    options.ClientSecret = settings.SecretKey;
//    options.ResponseType = OpenIdConnectResponseType.Code;
//    options.Scope.Add("openid");
//    options.Scope.Add("profile");
//    options.Scope.Add("email");
//    options.CallbackPath = "/signin-oidc";
//    options.SaveTokens = true;
//    options.RequireHttpsMetadata = false;
//});

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