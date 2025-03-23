using Microsoft.Extensions.DependencyInjection;
using UserControl.Core.Abstractions.Services;

namespace UserControl.Services;

public static class Ioc
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services.AddScoped<IUserService, UserService>();
    }
}
