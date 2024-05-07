using DataAccess.Commands;
using DataAccess.Commands.Contracts;
using DataAccess.Queries;
using DataAccess.Queries.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess;

public static class DependencyInjection
{
    private const string DefaultConnection = "DefaultConnection";

    public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(DefaultConnection);

        services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(connectionString,
                    o => o.UseNodaTime()
                );
            }
        );
        services.AddTransient<IToDoQuery, ToDoQuery>();
        services.AddTransient<IToDoCommand, ToDoCommand>();
    }
}