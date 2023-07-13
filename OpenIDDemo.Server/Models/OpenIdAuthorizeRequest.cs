namespace OpenIDDemo.Server.Models;

public class OpenIdAuthorizeRequest
{
    public string Password { get; set; }
    public string RedirectUri { get; set; }
    public string State { get; set; }
    public string Username { get; set; }
}