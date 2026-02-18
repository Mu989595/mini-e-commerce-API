using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Mini_E_Commerce_API.Data;
using Mini_E_Commerce_API.Models;
using Mini_E_Commerce_API.Services;

namespace Mini_E_Commerce_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. Add services to the container (Controllers)
            builder.Services.AddControllers();

            // 2. Add CORS policy with restricted origins
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    // For production, replace with actual domain
                    policy.WithOrigins("http://localhost:3000", "https://yourdomain.com")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });

            // 3. Add DbContext (Database connection configuration)
            builder.Services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions => sqlOptions
                        .EnableRetryOnFailure(maxRetryCount: 3, maxRetryDelay: TimeSpan.FromSeconds(10), errorNumbersToAdd: null)
                        .CommandTimeout(30)
                )
                .EnableSensitiveDataLogging(builder.Environment.IsDevelopment())
            );

            // 4. Add Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationContext>()
            .AddDefaultTokenProviders();

            // 5. Add Repository & Service Layer (Dependency Injection)
            builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<JWTService>();

            // 6. Add Logging
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();

            // 7. Add JWT Authentication
            var jwtSettings = builder.Configuration.GetSection("JWT");
            var secretKey = jwtSettings["SecretKey"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)),
                    ClockSkew = TimeSpan.Zero
                };
            });

            // --- Build the application ---
            var app = builder.Build();

            // 8. Run migrations on startup (optional, can be done manually)
            // using (var scope = app.Services.CreateScope())
            // {
            //     var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            //     db.Database.Migrate();
            // }

            // 9. Configure Middleware Pipeline
            // Enable CORS (must be before UseAuthentication and UseAuthorization)
            app.UseCors("AllowFrontend");

            // Keep HTTP convenient in development so localhost links open directly.
            if (!app.Environment.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }

            // Serve frontend pages from /frontend folder at the site root.
            var frontendPath = Path.Combine(app.Environment.ContentRootPath, "frontend");
            if (Directory.Exists(frontendPath))
            {
                var frontendProvider = new PhysicalFileProvider(frontendPath);
                app.UseDefaultFiles(new DefaultFilesOptions
                {
                    FileProvider = frontendProvider,
                    RequestPath = ""
                });
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = frontendProvider,
                    RequestPath = ""
                });
            }

            // Important order: Authentication must come before Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            // API info endpoint
            app.MapGet("/api", () => new { message = "Mini E-Commerce API", version = "1.0", endpoints = new { products = "/api/products", login = "/api/account/login", register = "/api/account/register" } });

            app.Run();
        }
    }
}
