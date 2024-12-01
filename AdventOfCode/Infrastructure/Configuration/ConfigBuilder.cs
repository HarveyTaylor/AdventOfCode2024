using Microsoft.Extensions.Configuration;

namespace AdventOfCode.Infrastructure.Configuration;

public static class ConfigBuilder
{
    public static AppConfiguration GetConfiguration()
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("app-settings.json")
            .AddJsonFile("app-settings.development.json", true)
            .SetBasePath(Path.GetDirectoryName(typeof(ConfigBuilder).Assembly.Location)!)
            .Build();

        var configuration = new AppConfiguration();
        config.Bind(configuration);

        return configuration;
    }
}