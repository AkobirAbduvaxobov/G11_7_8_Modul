using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoList.Application.Interfaces;
using TodoList.Infrastructure.Persistence;
using TodoList.Infrastructure.Persistence.Repositories;

namespace TodoList.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DatabaseConnection");

        services.AddDbContext<AppDbContext>(options =>
          options.UseSqlServer(connectionString));

        services.AddScoped<ITodoItemRepository, TodoItemRepository>();

        return services;
    }
}
