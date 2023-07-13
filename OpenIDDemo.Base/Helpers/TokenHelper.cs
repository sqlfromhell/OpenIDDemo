using Microsoft.IdentityModel.Logging;

namespace OpenIDDemo.Base.Helpers;

public static class TokenHelper
{
    public static string Get
        (TokenSettings settings, ClaimsPrincipal principal)
    {
        SymmetricSecurityKey key = new
            (Encoding.UTF8.GetBytes(settings.SecretKey));

        SigningCredentials credentials = new
            (key, SecurityAlgorithms.HmacSha256);

        IdentityModelEventSource.ShowPII = true;

        JwtSecurityToken jwtToken = new(
            expires: DateTime.UtcNow
                .AddSeconds(settings.Expires),
            claims: principal.Claims,
            signingCredentials: credentials
            );

        return new JwtSecurityTokenHandler()
            .WriteToken(jwtToken);
    }

    public static string GetLoginUrl
        (TokenSettings settings, string returnUrl)
        => GetServiceUrl(settings, "Account/Login", returnUrl);

    public static string GetLogoutUrl
        (TokenSettings settings, string returnUrl)
        => GetServiceUrl(settings, "Account/Logout", returnUrl);

    public static string GetReturnUrl
        (string token, string returnUrl)
    {
        UriBuilder uriBuilder
            = new(returnUrl);

        var query = HttpUtility
            .ParseQueryString
                (uriBuilder.Query);

        query["token"] = token;

        uriBuilder.Query = query
            .ToString();

        return uriBuilder
            .ToString();
    }

    private static string GetServiceUrl
        (TokenSettings settings, string relativeUrl, string returnUrl)
    {
        UriBuilder uriBuilder
            = new(settings.Service);

        uriBuilder.Path =
            string.IsNullOrEmpty(uriBuilder.Path)
            ? $"{uriBuilder.Path}/{relativeUrl}"
            : relativeUrl;

        var query = HttpUtility
            .ParseQueryString
                (uriBuilder.Query);

        query["returnUrl"] = returnUrl;

        uriBuilder.Query = query
            .ToString();

        return uriBuilder
            .ToString();
    }
}