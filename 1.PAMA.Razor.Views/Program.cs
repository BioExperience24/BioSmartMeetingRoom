using _6.Repositories.DB;
using _6.Repositories.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using static Global.Extensions.ServiceCollectionExtensions;

namespace PAMA1;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // DI HttpContext Acceessor
        builder.Services.AddHttpContextAccessor();

        // DI CSRF Token
        builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

        // Add services to the container.
        builder.Services.AddRazorPages()
            .AddJsonOptions(options =>
            {
                // Set agar property bernilai null tidak disertakan dalam JSON
                options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            });
        // Tambahkan konfigurasi DbContext di sini
        builder.Services.AddDbContext<MyDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped(sp =>
        {
            var options = sp.GetRequiredService<DbContextOptions<MyDbContext>>();
            return new MyDbContextFactory(options);
        });
        builder.Services.AddTransient<DalSession>();

        builder.Services.AddCustomService();
        builder.Services.AddCustomRepository();
        builder.Services.AddAutoMapper(typeof(AutoMapConfig));

        // Tambahin authentication dan authorization services
        builder.Services.AddAuthentication("CookieAuth")
            .AddCookie("CookieAuth", options =>
            {
                options.Cookie.Name = "AuthCookie";
                options.LoginPath = "/Authentication"; // Redirect ke sini kalau belum login
                options.AccessDeniedPath = "/AccessDenied";
            });

        builder.Services.AddAuthorization();

        #region SWAGGER
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

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
        #endregion

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        #region Swagger2
        app.UseSwagger();
        // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
        // specifying the Swagger JSON endpoint.
        app.UseSwaggerUI(c =>
        {
            /* default sesuai urutan swagger doc */
            //c.SwaggerEndpoint("/swagger/report/swagger.json", "Report API");
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "ITM CORE Dashboard & Report Portal");
            c.RoutePrefix = "firedocumentation";
            c.DocExpansion(DocExpansion.None);
            c.InjectStylesheet("/themes/theme-flattop.css");
            c.InjectJavascript("/themes/TabTitle.js");
        });
        app.MapControllers();
        #endregion

        app.UseRouting();

        // Pakai authentication & authorization middleware
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapRazorPages();
        app.Run();
    }
}
