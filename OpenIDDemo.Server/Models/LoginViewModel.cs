namespace OpenIDDemo.Server.Models;

public class LoginViewModel
{
    public LoginViewModel()
    { }

    public LoginViewModel(string returnUrl)
    {
        ReturnUrl = returnUrl;
    }

    public string Password { get; set; }
    public string ReturnUrl { get; set; }
    public string UserName { get; set; }
}