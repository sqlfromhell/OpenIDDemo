namespace OpenIDDemo.Server.Models;

public class OpenIdAuthorizeRequest
{
    public string RedirectUri { get; set; }
    public string State { get; set; }
}