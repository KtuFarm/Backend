using System;
using Backend.Configuration;
using Backend.Middleware;
using Backend.Models;
using Backend.Models.Common;
using Backend.Models.ManufacturerEntity;
using Backend.Models.MedicamentEntity;
using Backend.Models.PharmacyEntity;
using Backend.Models.ProductBalanceEntity;
using Backend.Models.UserEntity;
using Backend.Models.WorkingHoursEntity;
using Backend.Services.Validators.MedicamentDTOValidator;
using Backend.Services.Validators.PharmacyDTOValidator;
using Backend.Services.WorkingHoursManager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MySql.Data.MySqlClient;

namespace Backend
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = new MySqlConnectionStringBuilder(Configuration.GetConnectionString("DefaultConnection"))
            {
                Server = Configuration["Server"] ?? Environment.GetEnvironmentVariable("Server"),
                Database = Configuration["Database"] ?? Environment.GetEnvironmentVariable("Database"),
                UserID = Configuration["Uid"] ?? Environment.GetEnvironmentVariable("Uid"),
                Password = Configuration["DbPassword"] ?? Environment.GetEnvironmentVariable("DbPassword")
            };

            services.AddDbContext<ApiContext>(opt => opt.UseMySQL(builder.ConnectionString));
            services.AddControllers().AddNewtonsoftJson();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "KTU Farm", Version = "v1" });
                c.OperationFilter<SwaggerConfig>();
            });
            services.AddSwaggerGenNewtonsoftSupport();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", p =>
                {
                    p.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            RegisterCustomServices(services);
        }

        private static void RegisterCustomServices(IServiceCollection services)
        {
            services.AddScoped<IWorkingHoursManager, WorkingHoursManager>();
            services.AddScoped<IMedicamentDTOValidator, MedicamentDTOValidator>();
            services.AddScoped<IPharmacyDTOValidator, PharmacyDTOValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApiContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Backend v1"));
            }

            if (env.IsProduction())
            {
                context.Database.Migrate();
            }

            SeedDatabase(context);

            app.UseCors("AllowAll");

            app.UseRequestMiddleware();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private static void SeedDatabase(ApiContext context)
        {
            new Seeder(context, new ISeeder[]
            {
                new ManufacturerSeed(context),
                new WorkingHoursSeed(context),
                new MedicamentSeed(context),
                new PharmacySeed(context),
                new ProductBalanceSeed(context),
                new UserSeed(context)
            }).Seed();
        }
    }
}
