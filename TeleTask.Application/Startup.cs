using Microsoft.Extensions.DependencyInjection;

namespace TeleTask.Application;

public static class Startup
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Startup).Assembly));        
        
        return services;
    }
}
