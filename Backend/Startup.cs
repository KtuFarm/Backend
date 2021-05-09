using System;
using System.Text;
using API.Configuration;
using API.Services;
using Backend.Configuration;
using Backend.Middleware;
using Backend.Models;
using Backend.Models.Common;
using Backend.Models.ManufacturerEntity;
using Backend.Models.MedicamentEntity;
using Backend.Models.PharmacyEntity;
using Backend.Models.ProductBalanceEntity;
using Backend.Models.UserEntity;
using Backend.Models.WarehouseEntity;
using Backend.Models.WorkingHoursEntity;
using Backend.Services.HeadersValidator;
using Backend.Services.RequestValidator;
using Backend.Services.Jwt;
using Backend.Services.Validators.MedicamentDTOValidator;
using Backend.Services.Validators.PharmacyDTOValidator;
using Backend.Services.Validators.UserDTOValidator;
using Backend.Services.WorkingHoursManager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
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

            SetupSwagger(services);

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", p =>
                {
                    p.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            SetupAuthentication(services);
            RegisterCustomServices(services);
        }

        private static void SetupSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "KTU Farm", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });

                c.OperationFilter<SwaggerConfig>();
            });
            services.AddSwaggerGenNewtonsoftSupport();
        }

        private void SetupAuthentication(IServiceCollection services)
        {
            services.AddIdentityCore<User>();
            services.AddScoped<IUserStore<User>, AppUserStore>();
            services.AddScoped<IUserRoleStore<User>, AppUserStore>();

            string secret = Configuration.GetSection("Jwt").GetSection("SecretKey").Value;

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            ).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
                };
            });

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 8;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";
                options.User.RequireUniqueEmail = true;
            });
        }

        private static void RegisterCustomServices(IServiceCollection services)
        {
            services.AddScoped<IHeadersValidator, HeadersValidator>();
            services.AddScoped<IWorkingHoursManager, WorkingHoursManager>();
            services.AddScoped<IMedicamentDTOValidator, MedicamentDTOValidator>();
            services.AddScoped<IPharmacyDTOValidator, PharmacyDTOValidator>();
            services.AddScoped<IUserDTOValidator, UserDTOValidator>();
            services.AddScoped<IJwtService, JwtService>();
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

            app.UseAuthentication();

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
                new UserSeed(context),
                new WarehouseSeed(context)
            }).Seed();
        }
    }
}
