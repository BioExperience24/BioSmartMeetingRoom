using _1.PAMA.Razor.Views.Middlewares;
using _4.Data.ViewModels;
using _4.Helpers.Consumer;
using _5.Helpers.Consumer._AWS;
using _5.Helpers.Consumer.EnumType;
using _5.Helpers.Consumer.Policy;
using _6.Repositories.DB;
using _6.Repositories.Repository;
using _6.Repositories.Seeders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using System.Text;
using static Global.Extensions.ServiceCollectionExtensions;

namespace PAMA1;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        if (!builder.Environment.IsDevelopment())
        {
            var secretManager = new AwsSecretManagerService(builder.Configuration);
            await secretManager.LoadSecretsAsync();
        }

        var config = builder.Configuration;
        var httpsRedirectionEnabled = config.GetValue<bool>("HttpsRedirection:Enabled");

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

        builder.Services.AddHelperService(builder.Configuration);

        var configTokenM = builder.Configuration.GetSection("TokenManagement");
        builder.Services.Configure<TokenManagement>(configTokenM);
        TokenManagement token = configTokenM.Get<TokenManagement>() ?? new();
        builder.Services.Configure<CookiePolicyOptions>(options =>
        {
            options.Secure = CookieSecurePolicy.SameAsRequest; // Sesuai dengan request (HTTPS atau HTTP)
        });

        // Tambahin authentication dan authorization services
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = "CookieAuth"; // Default untuk Razor Pages
            //options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // Default untuk API
            options.DefaultChallengeScheme = "CookieAuth"; // Default challenge Razor Pages
        })
        .AddCookie("CookieAuth", options =>
        {
            options.Cookie.Name = "AuthCookie";
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; // Ikuti protokol
            options.Cookie.SameSite = SameSiteMode.Strict;
            options.LoginPath = "/Authentication"; // Redirect ke halaman login
            options.AccessDeniedPath = "/AccessDenied";
        }).AddJwtBearer(options =>
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
                        Message = "You do not have permission to access this resource.",
                        StatusCode = 403
                    };

                    return context.Response.WriteAsJsonAsync(response);
                }
            };
        });

        builder.Services.AddAuthorization(AuthorizationWebviewPolicies.AddCustomPolicies);

        string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
        var databaseName = connectionStringBuilder.InitialCatalog;

        builder.Services.AddDistributedSqlServerCache(options =>
        {
            options.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            options.SchemaName = databaseName;
            options.TableName = "sessions";
        });

        // builder.Services.AddStackExchangeRedisCache(options =>
        // {
        //     options.Configuration = builder.Configuration.GetValue<string>("Redis:ConnectionString");
        //     options.InstanceName = "pama-smr:";
        // });


        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromSeconds(token.AccessExpiration); // Timeout session
            options.Cookie.HttpOnly = true; // Mencegah akses JavaScript ke cookie
            options.Cookie.IsEssential = true; // Tetap tersimpan meskipun user menolak cookies opsional
            options.Cookie.SameSite = SameSiteMode.Strict; // Mencegah pengiriman cookie ke situs lain (CSRF protection)
            // options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Hanya mengirim cookie melalui HTTPS 
        });
        // builder.Services.AddDistributedMemoryCache(); // Simpan session di memory
        // builder.Services.AddSession(options =>
        // {
        //     options.Cookie.HttpOnly = true;
        //     options.Cookie.IsEssential = true;
        //     options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        //     options.Cookie.SameSite = SameSiteMode.Strict;
        //     options.IdleTimeout = TimeSpan.FromSeconds(token.AccessExpiration);
        // });
        #region SWAGGER

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddHttpClient("MyClient", c =>
        {
            // Configure your client here...
        });
        builder.Services.AddScoped<APICaller>(); // Register APICaller properly

        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        // Configure Data Protection to use a shared directory in PVC
        builder.Services.AddDataProtection()
            .PersistKeysToFileSystem(new DirectoryInfo(@"/app/DataProtection-Keys"))
            .SetApplicationName("pama-smr");

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "PAMA SMeet Room",
                Version = @$"last build backend: {File.GetLastWriteTime($"{AppDomain.CurrentDomain.BaseDirectory}{Assembly.GetEntryAssembly().GetName().Name}.dll").ToLocalTime().ToString("dd MMM yyyy HH:mm:ss \"GMT\"zzz")}",
                Description = "Swagger NET 8"
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

            c.OperationFilter<SecurityRequirementsOperationFilter>();
            c.DocInclusionPredicate((docName, apiDesc) =>
            {
                if (docName == "v1")
                {
                    return true;
                }
                else return true;
            });

        });
        #endregion

        var app = builder.Build();

        // run database seeder
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<MyDbContext>();
            DbSeeder.Seed(db, builder.Configuration);
        }

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
        //app.UseSwaggerAuthorized();
        //var setToken = PublicAccess.getToken;
        app.UseSwaggerUI(c =>
        {
            /* default sesuai urutan swagger doc */
            //c.SwaggerEndpoint("/swagger/report/swagger.json", "Report API");
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "CMS API");
            c.RoutePrefix = "firedocumentation";
            c.DocExpansion(DocExpansion.None);
            c.InjectStylesheet("/themes/theme-flattop.css");
            c.InjectJavascript("/themes/TabTitle.js");
        });
        app.MapControllers();
        #endregion

        // Menambahkan middleware session
        app.UseSession();
        
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedProto
        });

        app.UseRouting();

        // Pakai authentication & authorization middleware
        app.UseAuthentication();
        app.UseAuthorization();

        // Custom Middleware
        app.UseErrorHandling();

        app.MapRazorPages();
        app.MapGet("/health", () => Results.Ok("Healthy"));
        app.Run();
    }
}
