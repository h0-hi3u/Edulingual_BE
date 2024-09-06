using Edulingual.Common.Helper;
using Edulingual.Common.Interfaces;
using Edulingual.DAL.Data;
using Edulingual.DAL.Interfaces;
using Edulingual.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Edulingual.IntegrationTests;

public static class DISetup
{
    public static IServiceProvider ServiceProvider { get; private set; }
    public static IConfiguration InitConfiguration()
    {
        var config = new ConfigurationBuilder()
           .AddJsonFile("appsettings.Test.json")
           .AddEnvironmentVariables()
           .Build();
        return config;
    }
    public static void InitializeServices()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        DatabaseHelper.InitConfiguration(InitConfiguration());
        var services = new ServiceCollection();
        services.AddScoped<IApplicationDbContext, EdulingualContext>();
        services.AddScoped<EdulingualContext>();
        services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<ICurrentUser, CurrentUser>();

        var registerableTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => typeof(IAutoRegisterable).IsAssignableFrom(type) && type.IsInterface)
            .ToList();

        foreach (var type in registerableTypes)
        {
            var implemetationType = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .FirstOrDefault(t => type.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
            if (implemetationType != null)
                services.AddScoped(type, implemetationType);

        }
        ServiceProvider = services.BuildServiceProvider();
    }
    public static T GetRequiredService<T>()
    {
        return ServiceProvider.GetRequiredService<T>();
    }
}
