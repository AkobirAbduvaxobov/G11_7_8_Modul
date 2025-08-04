using Microsoft.EntityFrameworkCore;
using TodoList.Infrastructure.Persistence;

namespace TodoList.Server.Configurations;

public static class DatabaseConfiguration
{
    public static void ConfigureDB(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");

        builder.Services.AddDbContext<AppDbContext>(options =>
          options.UseSqlServer(connectionString));
    }
}
