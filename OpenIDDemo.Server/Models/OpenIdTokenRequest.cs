namespace OpenIDDemo.Server.Models;

public class OpenIdTokenRequest
{
    public string ClientId { get; set; }
    public string Code { get; set; }
    public string GrantType { get; set; }
    public string Password { get; internal set; }
    public string RedirectUri { get; set; }
    public string UserName { get; internal set; }
}