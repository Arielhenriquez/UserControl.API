using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserControl.Model.Context;

namespace UserControl.Model;

public static class Ioc
{
    public static IServiceCollection AddPostgres(this IServiceCollection services, string connectionString)
    {
        return services
          .AddDbContext<UserContactDbContext>((provider, builder) => builder.UseNpgsql(connectionString));
    }
}
