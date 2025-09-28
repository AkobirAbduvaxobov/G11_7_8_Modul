using CqrsBook.Application.Interfaces;
using CqrsBook.Infrastructure.Persistence;
using CqrsBook.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CqrsBook.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        var sqlConnectionString = configuration.GetConnectionString("DatabaseConnection");

        services.AddDbContext<AppDbContext>(options =>
          options.UseSqlServer(sqlConnectionString));

        services.AddScoped<IBookReadRepository, BookReadRepository>();
        services.AddScoped<IBookWriteRepository, BookWriteRepository>();

        return services;
    }
}
