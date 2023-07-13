using OpenIDDemo.Server.Models;
using System.Web;

namespace OpenIDDemo.Tests;

public class Tests
{
    public const string CONTENT_TYPE_JSON = "application/json";
    public FakeClient CliApp { get; set; }
    public HttpClient CliClient { get; set; }
    public FakeServer SrvApp { get; set; }
    public HttpClient SrvClient { get; set; }
    public string Step_002_LocalUrl { get; set; }
    public string Step_003_LocalUrl { get; set; }
    public string Step_005_LocalUrl { get; set; }
    public string Step_005_ReturnUrl { get; set; }

    [SetUp]
    public async Task SetupAsync()
    {
        SrvApp = new();

        SrvClient = SrvApp.CreateClient();

        CliApp = new();

        CliClient = CliApp.CreateClient();
    }

    [Test, Order(001)]
    public async Task Step_001_HomeIndex()
    {
        // Arrange

        // Act

        var page = await CliClient
            .GetAsync("/Home/Index");

        // Assert

        Assert.That(page.StatusCode,
            Is.EqualTo(HttpStatusCode.OK));
    }

    [Test, Order(002)]
    public async Task Step_002_HomeProtected()
    {
        // Arrange

        // Act

        var page = await CliClient
            .GetAsync("/Home/Protected");

        // Assert

        Assert.That(page.StatusCode,
            Is.EqualTo(HttpStatusCode.Redirect));

        var url = page.Headers.Location;

        Step_002_LocalUrl = url.LocalPath;

        Assert.That(Step_002_LocalUrl,
            Is.EqualTo("/Account/Login"));

        var query = HttpUtility
           .ParseQueryString(url.Query);

        Step_002_LocalUrl +=
            "?" + query;
    }

    [Test, Order(003)]
    public async Task Step_003_AccountLogin()
    {
        // Arrange

        // Act

        var page = await CliClient
            .GetAsync(Step_002_LocalUrl);

        // Assert

        Assert.That(page.StatusCode,
            Is.EqualTo(HttpStatusCode.Redirect));

        var url = page.Headers.Location;

        Assert.That(url.Host,
            Is.EqualTo(SrvClient.BaseAddress.Host));

        Step_003_LocalUrl = url.LocalPath;

        Assert.That(Step_003_LocalUrl,
            Is.EqualTo("/Account/Login"));

        var query = HttpUtility
           .ParseQueryString(url.Query);

        Step_005_LocalUrl = url.LocalPath;

        Step_005_ReturnUrl = query["returnUrl"];

        Step_003_LocalUrl +=
            "?" + query;
    }

    [Test, Order(004)]
    public async Task Step_004_AccountLogin()
    {
        // Arrange

        // Act

        var page = await SrvClient
            .GetAsync(Step_003_LocalUrl);

        // Assert

        Assert.That(page.StatusCode,
            Is.EqualTo(HttpStatusCode.OK));
    }

    [Test, Order(005)]
    public async Task Step_005_AccountLogin_Post()
    {
        // Arrange

        LoginViewModel rq = new()
        {
            UserName = "jane",
            Password = "123456",
            ReturnUrl = Step_005_ReturnUrl
        };

        HttpRequestMessage httpRq = new
            (HttpMethod.Post, Step_005_LocalUrl)
        {
            Content = new FormUrlEncodedContent(
                new KeyValuePair<string, string>[]
                {
                    new (nameof(LoginViewModel.UserName), rq.UserName),
                    new (nameof(LoginViewModel.Password), rq.Password),
                    new (nameof(LoginViewModel.ReturnUrl), rq.ReturnUrl)
                }
            )
        };

        // Act

        var page = await SrvClient
            .SendAsync(httpRq);

        // Assert

        Assert.That(page.StatusCode,
            Is.EqualTo(HttpStatusCode.Redirect));

        var url = page.Headers.Location;
    }
}