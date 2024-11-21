
using _6.Repositories.DB;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using static Global.Extensions.ServiceCollectionExtensions;

namespace _2.Web.API.Controllers;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Tambahkan konfigurasi DbContext di sini
        builder.Services.AddDbContext<MyDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddCustomService();
        builder.Services.AddCustomRepository();
        builder.Services.AddAutoMapper(typeof(AutoMapConfig));

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "PAMA SMeet Room",
                Version = @$"last build backend: {File.GetLastWriteTime($"{AppDomain.CurrentDomain.BaseDirectory}{Assembly.GetEntryAssembly().GetName().Name}.dll").ToLocalTime().ToString("dd MMM yyyy HH:mm:ss \"GMT\"zzz")}",
                Description = "Swagger"
            });
            // Configure Swagger to use the xml documentation file
            //var xmlFile = Path.ChangeExtension(typeof(Startup).Assembly.Location, ".xml");
            //c.IncludeXmlComments(xmlFile);
            c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
        });

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
