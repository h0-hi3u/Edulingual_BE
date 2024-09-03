using AutoMapper;
using Edulingual.Common.Interfaces;
using Edulingual.DAL.Data;
using Edulingual.DAL.Interfaces;
using Edulingual.Infrastructure;
using Edulingual.Service.AutoMapper;
using Edulingual.Service.Library;
using Microsoft.Extensions.DependencyInjection;

namespace Edulingual.Service.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IApplicationDbContext, EdulingualContext>();
        services.AddScoped<EdulingualContext>();
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddSingleton<VnPayLibrary>();

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
        var config = new MapperConfiguration(AutoMapperConfiguration.RegisterMaps);
        var mapper = config.CreateMapper();
        services.AddSingleton(mapper);

        return services;
    }
}
