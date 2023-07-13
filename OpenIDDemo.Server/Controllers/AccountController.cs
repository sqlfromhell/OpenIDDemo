namespace OpenIDDemo.Server.Controllers;

public class AccountController
    : Controller
{
    private const string TOKEN =
        CookieAuthenticationDefaults.AuthenticationScheme;

    private readonly TokenSettings TokenSettings;
    private readonly IUserRepository UserRepository;

    public AccountController(
        TokenSettings tokenSettings,
        IUserRepository userRepository
    )
    {
        TokenSettings = tokenSettings;
        UserRepository = userRepository;
    }

    [HttpGet]
    public IActionResult Login
        ([FromQuery] string returnUrl)
        => HttpContext.User is not null
        && HttpContext.User.Identity.IsAuthenticated
        ? ReturnCallback(returnUrl)
        : View("Login",
            new LoginViewModel()
            {
                ReturnUrl = returnUrl
            });

    [HttpPost]
    public async Task<IActionResult> LoginAsync
        (LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = UserRepository
                .Get(model.UserName, model.Password);

            if (user is not null)
            {
                await HttpContext
                    .SignInAsync(TOKEN, user
                        .GetPrincipal(TOKEN));

                return RedirectToAction("ReturnCallback",
                    new { returnUrl = model.ReturnUrl });
            }

            ModelState.AddModelError
                ("", "Invalid username or password");
        }

        return View("Login", model);
    }

    [HttpGet]
    public async Task<IActionResult> LogoutAsync
        ([FromQuery] string returnUrl)
    {
        await HttpContext
            .SignOutAsync();

        return string.IsNullOrEmpty(returnUrl)
            ? RedirectToAction("Index", "Home")
            : Redirect(returnUrl);
    }

    public IActionResult ReturnCallback
        (string returnUrl)
    {
        var principal = HttpContext.User;

        if (principal is null
            || !HttpContext.User.Identity.IsAuthenticated)
            return Unauthorized();

        var token = TokenHelper
            .Get(TokenSettings, principal);

        var url = TokenHelper
            .GetReturnUrl(token, returnUrl);

        return url is null
            ? RedirectToAction("Protected", "Home")
            : Redirect(url);
    }
}