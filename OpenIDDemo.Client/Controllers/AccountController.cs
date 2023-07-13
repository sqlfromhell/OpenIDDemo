using OpenIDDemo.Base.Helpers;

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
        ([FromQuery] string token)
    {
        var principal =
            TokenSettings.GetPrincipal(token);

        if (principal is null)
            return Unauthorized();

        await HttpContext
            .SignInAsync(TOKEN, principal);

        return RedirectToAction("Protected", "Home");
    }

    public async Task<IActionResult> CallbackLogoutAsync()
    {
        await HttpContext
            .SignOutAsync(TOKEN);

        return RedirectToAction("Index", "Home");
    }

    public IActionResult Login()
    {
        var returnUrl = Url
            .Action("Callback", "Account", null,
                protocol: TokenSettings.Protocol);

        var url = TokenHelper.GetLoginUrl
            (TokenSettings, returnUrl);

        return Redirect(url);
    }

    public IActionResult Logout()
    {
        var returnUrl = Url
            .Action("CallbackLogout", "Account", null,
                protocol: TokenSettings.Protocol);

        var url = TokenHelper.GetLogoutUrl
            (TokenSettings, returnUrl);

        return Redirect(url);
    }
}