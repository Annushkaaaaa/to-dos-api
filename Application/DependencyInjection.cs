using Application.Services;
using Application.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IToDoService, ToDoService>();
    }
}