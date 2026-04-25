
using Company.BLL.Profiles;
using Company.BLL.Services.Classes.Auth;
using Company.BLL.Services.Classes.Employees;
using Company.BLL.Services.Interfaces.Employees;
using Company.BLL.Services.Interfaces.IAuth;
using Company.DAL.Data.Contexts;
using Company.DAL.Data.Repositories.Classes;
using Company.DAL.Data.Repositories.Interfaces;
using Company.DAL.Entities;
using Company.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Text;

namespace Company.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Company API", Version = "v1" });

                // Add JWT Authentication to Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
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
            new string[] {}
        }
    });
            });
            
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Read JwtOptions from configuration
            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
            var jwtOptions = builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>();

            // Add JWT authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = ctx =>
                    {
                        Console.WriteLine("Auth failed: " + ctx.Exception.Message);
                        return Task.CompletedTask;
                    },
                    OnChallenge = ctx =>
                    {
                        Console.WriteLine("Challenge error: " + ctx.ErrorDescription);
                        return Task.CompletedTask;
                    }
                };
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtOptions.SecurityKey))
                };
            });


            builder.Services.AddScoped<IEmployeeManager, EmployeeManager>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            // Register Identity
            builder.Services.AddIdentityCore<AppUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()   // use your EF DbContext for persistence
                .AddDefaultTokenProviders();                // enable token generation (reset, confirm, etc.)
            

            builder.Services.AddScoped<IAuthManager, AuthManager>();
            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));
           

            //builder.Services.AddScoped<DepartmentResolver>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

           



            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
