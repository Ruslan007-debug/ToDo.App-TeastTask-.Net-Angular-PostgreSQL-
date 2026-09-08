
using Microsoft.EntityFrameworkCore;
using System;
using ToDo.Api.DataAccess.Data;

namespace ToDo.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            DotNetEnv.Env.Load();
            var connectionString = $"Host={Environment.GetEnvironmentVariable("DB_HOST")};" +
                                   $"Port={Environment.GetEnvironmentVariable("DB_PORT")};" +
                                   $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
                                   $"Username={Environment.GetEnvironmentVariable("DB_USER")};" +
                                   $"Password={Environment.GetEnvironmentVariable("DB_PASS")}";

            builder.Services.AddDbContext<ToDoDbContext>(options => 
            options.UseNpgsql(connectionString));

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
