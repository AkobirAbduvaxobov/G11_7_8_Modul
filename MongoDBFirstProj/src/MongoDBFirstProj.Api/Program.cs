
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDBFirstProj.Api.Services;

namespace MongoDBFirstProj.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // MongoDB
        var mongoConnectionString = builder.Configuration.GetConnectionString("MongoDBConnection");
        var mongoClient = new MongoClient(mongoConnectionString);
        var mongoDatabase = mongoClient.GetDatabase("BookStoreG11");
        builder.Services.AddSingleton<IMongoDatabase>(mongoDatabase);

        //
        builder.Services.AddScoped<IMongoBookService, MongoBookService>();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
