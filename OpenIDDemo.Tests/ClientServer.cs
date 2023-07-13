namespace OpenIDDemo.Tests;

public class FakeClient
    : WebApplicationFactory<Client.Program>
{
    public string HostUrl { get; set; }
        = "http://localhost:4078";

    protected override void ConfigureWebHost
        (IWebHostBuilder builder)
        => builder.UseUrls(HostUrl);

    protected override IHost CreateHost
        (IHostBuilder builder)
    {
        var host = builder
            .Build();

        host.Start();

        return host;
    }
}