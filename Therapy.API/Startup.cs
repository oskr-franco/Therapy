using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text;
using Therapy.API.Middleware;
using Therapy.Core.Mappings;
using Therapy.Core.Services.Accounts;
using Therapy.Core.Services.AuthAccessor;
using Therapy.Core.Services.Exercises;
using Therapy.Core.Services.Tokens;
using Therapy.Core.Services.Users;
using Therapy.Core.Services.Workouts;
using Therapy.Domain.Entities;
using Therapy.Infrastructure.Data;
using Therapy.Infrastructure.Repositories;

namespace TherapyAPI
{
    public class Startup
    {
      public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TherapyDbContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IAuthAccessorService, AuthAccessorService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IExerciseService, ExerciseService>();
            services.AddTransient<IWorkoutService, WorkoutService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IAccountService, AccountService>();

            services.AddAutoMapper(typeof(ExerciseMappingProfile));

            // Configure services, dependencies, etc.
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            // JWT configuration
            var TokenSettingsSection = Configuration.GetSection("TokenSettings");
            services.Configure<TokenSettings>(TokenSettingsSection);
            var token = TokenSettingsSection.Get<TokenSettings>();
            var key = Encoding.UTF8.GetBytes(token.Secret);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ValidAudience = token.Audience,
                        ValidIssuer = token.Issuer,
                        ClockSkew = TimeSpan.Zero, // No tolerance for expiration time.
                        RequireExpirationTime = true, // Require a token to have an expiration time.
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine("OnAuthenticationFailed: " +
                                context.Exception.Message);
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            Console.WriteLine("OnTokenValidated: " +
                                context.SecurityToken);
                            return Task.CompletedTask;
                        }
                    };
                });

            // Swagger configuration
            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo {
                    Version = "v1",
                    Title = "Therapy API",
                    Description = "The Therapy API is an ASP.NET Core Web API for managing therapy exercises.  The API is designed to be used by therapists and their patients to track progress and manage therapy exercises.",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Oscar Franco",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Example License",
                        Url = new Uri("https://example.com/license")
                    }
                });
                // using System.Reflection;
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
                options.ExampleFilters();
            });
            services.AddSwaggerExamplesFromAssemblyOf<Startup>();
            // End Swagger configuration
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (true)
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            // app.UsePathBase(new PathString("/api"));
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "api/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
