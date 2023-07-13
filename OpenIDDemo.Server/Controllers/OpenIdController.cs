//using OpenIDDemo.Server.Helpers;

//namespace OpenIDDemo.Server.Controllers;

//[Route("connect")]
//[ApiController]
//public class OpenIdController
//    : ControllerBase
//{
//    private const string TOKEN =
//        CookieAuthenticationDefaults.AuthenticationScheme;

//    private readonly TokenSettings TokenSettings;

//    public OpenIdController
//        (TokenSettings tokenSettings)
//        => TokenSettings = tokenSettings;

//    [HttpGet("authorize")]
//    public IActionResult Authorize
//        ([FromQuery] OpenIdAuthorizeRequest request)
//    {
//        var redirectUrl = $"{request.RedirectUri}?code=your-authorization-code&state={request.State}";

//        return Redirect(redirectUrl);
//    }

//    [HttpGet("logout")]
//    public IActionResult Logout
//        ([FromQuery] string postLogoutRedirectUri)
//    {
//        var redirectUrl = string.IsNullOrEmpty(postLogoutRedirectUri)
//            ? "/"
//            : postLogoutRedirectUri;

//        return Redirect(redirectUrl);
//    }

//    [HttpPost("token")]
//    public IActionResult Token
//        ([FromForm] OpenIdTokenRequest model)
//    {
//        if (model.UserName == "admin"
//            && model.Password == "password")
//        {
//            List<Claim> claims = new()
//            {
//                new (ClaimTypes.Name, model.UserName),
//            };

//            ClaimsIdentity identity =
//                new(claims, TOKEN);

//            ClaimsPrincipal principal =
//                new(identity);

//            return Ok(new OpenIdTokenResponse()
//            {
//                AccessToken = TokenHelper
//                    .Get(TokenSettings, principal),
//                ExpiresIn =
//                    TokenSettings.Expires
//            });
//        }

//        return Unauthorized();
//    }

//    [HttpGet("userinfo")]
//    public IActionResult UserInfo
//        ([FromQuery] string accessToken)
//    {
//        var principal =
//            TokenSettings
//                .GetPrincipal(accessToken);

//        if (principal is null)
//            return Unauthorized();

//        var userInfo =
//            principal.Claims
//                .ToDictionary(e => e.Type, e => e.Value);

//        return Ok(userInfo);
//    }
//}