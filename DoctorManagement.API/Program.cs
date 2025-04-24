
using DoctorManagement.BusinessLogicLayer;
using DoctorManagement.DataAccessLayer;
using DoctorManagement.Shared.Constants;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DoctorManagement.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddCors(config =>
            {
                config.AddDefaultPolicy(p =>
                {
                    p.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
                    
                });
            });
            builder.Services
                .AddAuthentication()
                .AddCookie(config=>
                {
                    config.LoginPath = "/";
                });
            builder.Services.AddHttpClient(WebApiNameConstants.AppApi, config =>
            {
                config.BaseAddress = new Uri(builder.Configuration["WebApiUrls:Default"] ?? string.Empty);
                
            });
           
            builder.Services.AddDbContext<WebDbContext>(config =>
            {
                config.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            }).AddIdentity<IdentityUser, IdentityRole>(config =>
            {
                config.Password.RequireLowercase = false;
                config.Password.RequiredLength = 4;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                config.Password.RequireDigit = false;
            })
            .AddEntityFrameworkStores<WebDbContext>()
            .AddDefaultTokenProviders()
            .AddApiEndpoints();

            builder.Services.AddBusinessLogicServices();
            builder.Services.AddControllers(options =>
            {
                //options.
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors();
            app.UseAuthorization();


            app.MapControllers();
            app.MapIdentityApi<IdentityUser>();
            app.Run();
        }
    }
}
