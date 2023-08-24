using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Therapy.API.Middleware;
using Therapy.Core.Mappings;
using Therapy.Core.Services.Exercises;
using Therapy.Core.Services.Workouts;
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

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IExerciseService, ExerciseService>();
            services.AddTransient<IWorkoutService, WorkoutService>();

            services.AddAutoMapper(typeof(ExerciseMappingProfile));

            // Configure services, dependencies, etc.
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            // Swagger configuration
            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
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
            });

            // End Swagger configuration
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            // app.UsePathBase(new PathString("/api"));
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseRouting();

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
