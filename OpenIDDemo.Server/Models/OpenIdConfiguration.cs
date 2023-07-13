namespace OpenIDDemo.Server.Models;

public class OpenIdConfiguration
{
    public string AuthorizationEndpoint { get; set; }
    public string Issuer { get; set; }
    public string LogoutEndpoint { get; set; }
    public string TokenEndpoint { get; set; }
    public string UserInfoEndpoint { get; set; }
}