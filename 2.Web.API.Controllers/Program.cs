
using _4.Data.ViewModels;
using _4.Helpers.Consumer;
using _5.Helpers.Consumer._AWS;
using _5.Helpers.Consumer.EnumType;
using _5.Helpers.Consumer.Policy;
using _6.Repositories.DB;
using _6.Repositories.Repository;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using System.Text;
using static Global.Extensions.ServiceCollectionExtensions;

namespace _2.Web.API.Controllers;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        if (!builder.Environment.IsDevelopment() && !builder.Environment.IsEnvironment("Staging"))
        {
            var secretManager = new AwsSecretManagerService(builder.Configuration);
            await secretManager.LoadSecretsAsync();
        }
        // Add services to the container.
        // Tambahkan konfigurasi DbContext di sini
        builder.Services.AddDbContext<MyDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        // DI HttpContext Acceessor
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddCustomService();
        builder.Services.AddCustomRepository();
        builder.Services.AddAutoMapper(typeof(AutoMapConfig));
        var apiVersion = builder.Configuration.GetSection("OTHER_SETTING").GetValue<string>("ApiVersion") ?? "v1";
        var configTokenM = builder.Configuration.GetSection("TokenManagement");

        builder.Services.Configure<TokenManagement>(configTokenM);
        TokenManagement token = configTokenM.Get<TokenManagement>() ?? new();

        // Tambahin authentication dan authorization services
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // Default untuk API
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token.SecretKey)),
                ValidIssuer = token.Issuer,
                ValidAudience = token.Audience,
                ValidateIssuer = true,
                ValidateAudience = true
            };
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];
                    var path = context.HttpContext.Request.Path;
                    if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/notifhub"))
                    {
                        context.Token = accessToken;
                    }
                    return Task.CompletedTask;
                },
                OnChallenge = context =>
                {
                    context.HandleResponse();
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";

                    var response = new ReturnalModel
                    {
                        Title = ReturnalType.UnAuthorized,
                        Status = ReturnalType.UnAuthorized,
                        Message = "Unauthorized Error Message: Token is invalid or missing.",
                        StatusCode = 401
                    };

                    return context.Response.WriteAsJsonAsync(response);
                },
                OnForbidden = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    context.Response.ContentType = "application/json";

                    var response = new ReturnalModel
                    {
                        Title = ReturnalType.Forbidden,
                        Status = ReturnalType.Forbidden,
                        Message = "Failed, your access is restricted.",
                        StatusCode = 403
                    };

                    return context.Response.WriteAsJsonAsync(response);
                }
            };
        });

        builder.Services.AddAuthorization(AuthorizationWebviewPolicies.AddCustomPolicies);

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen(c =>
        {
            // Webview
            c.SwaggerDoc("webview", new OpenApiInfo
            {
                Title = "Webview API",
                Version = "v1"
            });

            // Admin
            c.SwaggerDoc("pama_smeet", new OpenApiInfo
            {
                Title = "Pama Smart Meeting Room API",
                Version = "v1"
            });

            c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            c.OperationFilter<SecurityRequirementsOperationFilter>();

            // Show APIs only if controller matches group name
            c.DocInclusionPredicate((docName, apiDesc) =>
            {
                var groupName = apiDesc.GroupName;
                return groupName == docName;
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "oauth2"
                }
            },
            Array.Empty<string>()
        }
            });
        });


        builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
        {
            builder.AllowAnyHeader()
                   .AllowAnyMethod()
                   .SetIsOriginAllowed((host) => true)
                   .AllowCredentials();
        }));
        builder.Services.AddHttpClient("MyClient", c =>
        {
            // Configure your client here...
        });
        builder.Services.AddScoped<APICaller>(); // Register APICaller properly

        builder.Services.AddHelperService(builder.Configuration);

        var app = builder.Build();

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        #region Swagger2
        app.UseSwagger();
        // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
        // specifying the Swagger JSON endpoint.
        //app.UseSwaggerAuthorized();
        //var setToken = PublicAccess.getToken;
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/pama_smeet/swagger.json", "Pama Smart Meeting Room API");
            c.SwaggerEndpoint("/swagger/webview/swagger.json", "Webview API");

            c.RoutePrefix = "firedocumentation";
            c.DocExpansion(DocExpansion.None);
            c.InjectStylesheet("/custom/SwaggerDark.css");
            c.InjectJavascript("/custom/SwaggerUICustom.js");
        });

        app.MapControllers();
        #endregion

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors("MyPolicy");
        // Tambahin prefix route otomatis

        app.Run();
    }
}