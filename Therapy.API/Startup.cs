using Microsoft.EntityFrameworkCore;
using PhysicalTherapy.Core.Mappings;
using Therapy.Core.Services;
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

            services.AddAutoMapper(typeof(MappingProfile));

            // Configure services, dependencies, etc.
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
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
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
