using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace UnitTests;

public class Startup
{
#pragma warning disable CA1822 // called by the testing framework
    public static void ConfigureHost(IHostBuilder hostBuilder)
    {
        var config = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();

        hostBuilder.ConfigureHostConfiguration(builder => builder.AddConfiguration(config));
    }
#pragma warning restore CA1822 // called by the testing framework
}