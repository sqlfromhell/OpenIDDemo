namespace OpenIDDemo.Client.Controllers;

public class AccountController
    : Controller
{
    private const string TOKEN =
        CookieAuthenticationDefaults.AuthenticationScheme;

    private readonly TokenSettings TokenSettings;

    public AccountController
        (TokenSettings tokenSettings)
        => TokenSettings = tokenSettings;

    public async Task<IActionResult> CallbackAsync
        ([FromQuery] string token, [FromQuery] string returnUrl = null)
    {
        var principal =
            TokenSettings.GetPrincipal(token);

        if (principal is null)
            return Unauthorized();

        await HttpContext
            .SignInAsync(TOKEN, principal);

        return string.IsNullOrEmpty(returnUrl)
            ? RedirectToAction("Protected", "Home")
            : Redirect(returnUrl);
    }

    public async Task<IActionResult> CallbackLogoutAsync
        ([FromQuery] string returnUrl = null)
    {
        await HttpContext
            .SignOutAsync(TOKEN);

        return string.IsNullOrEmpty(returnUrl)
            ? RedirectToAction("Index", "Home")
            : Redirect(returnUrl);
    }

    public IActionResult Login
        ([FromQuery] string returnUrl)
    {
        var callbackUrl = Url
            .ActionLink("Callback", "Account",
                new { returnUrl },
                HttpContext.Request.Scheme
            );

        var url = TokenHelper.GetLoginUrl
            (TokenSettings, callbackUrl);

        return Redirect(url);
    }

    public IActionResult Logout
        ([FromQuery] string returnUrl)
    {
        var callbackUrl = Url
            .ActionLink("CallbackLogout", "Account",
                new { returnUrl },
                HttpContext.Request.Scheme
            );

        var url = TokenHelper.GetLogoutUrl
            (TokenSettings, callbackUrl);

        return Redirect(url);
    }
}