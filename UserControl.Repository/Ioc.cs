using Microsoft.Extensions.DependencyInjection;
using UserControl.Core.Abstractions.Repositories;

namespace UserControl.Repository;

public static class Ioc
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return
          services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
    }
}
