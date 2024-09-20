using React_ASPNet.Models;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;

namespace React_ASPNet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Env.Load();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            string connectionString = Environment.GetEnvironmentVariable("SQLSTRING");

            builder.Services.AddDbContext<ReactAspNetContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("New", app => 
                {
                    app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("New");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
