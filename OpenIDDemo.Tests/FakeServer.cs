namespace OpenIDDemo.Tests;

public class FakeServer
    : WebApplicationFactory<Server.Program>
{
    public string HostUrl { get; set; }
        = "http://localhost:4077";

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