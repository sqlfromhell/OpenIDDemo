namespace OpenIDDemo.Client.Controllers;

public class HomeController
    : Controller
{
    /// <summary>
    /// Anonymous method accessible to all users
    /// </summary>
    [AllowAnonymous]
    public IActionResult Index()
        => Ok("This is a public method accessible to all users.");

    /// <summary>
    /// Authorized method accessible only to authenticated users
    /// </summary>
    [Authorize]
    public IActionResult Protected()
        => Ok("This is a protected method accessible only to authenticated users.");
}