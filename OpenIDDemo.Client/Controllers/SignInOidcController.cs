//namespace OpenIDDemo.Client.Controllers;

//public class SignInOidcController
//    : Controller
//{
//    [HttpGet("/signin-oidc")]
//    public async Task<IActionResult> GetAsync()
//    {
//        var result = await HttpContext.AuthenticateAsync
//            (CookieAuthenticationDefaults.AuthenticationScheme);

//        return result?.Succeeded == true
//            ? RedirectToAction("Authorized", "Home")
//            : RedirectToAction("Error", "Home");
//    }
//}