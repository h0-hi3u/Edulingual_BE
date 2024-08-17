using Serilog;

namespace Edulingual.Api.Extensions;

public static class WebApplicationBuilderExtension
{
    public static void AddSerilog(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        Log.Logger = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .WriteTo.Debug()
                    .WriteTo.Console()
                    .Enrich.WithProperty("Environment", builder.Environment.EnvironmentName)
                    .ReadFrom.Configuration(builder.Configuration)
                    .CreateLogger();
        builder.Host.UseSerilog();
    }
}
