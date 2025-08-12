
using DoctorManagement.Api.Consumer;
using DoctorManagement.BusinessLogicLayer;
using DoctorManagement.DataAccessLayer;
using DoctorManagement.Shared.Constants;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

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
                .AddAuthentication(config =>
                {
                    config.DefaultScheme = IdentityConstants.BearerScheme;
                    config.DefaultAuthenticateScheme = IdentityConstants.BearerScheme;
                    
                })
                .AddBearerToken(IdentityConstants.BearerScheme, config =>
                {
                    config.BearerTokenExpiration = TimeSpan.FromMinutes(60);
                });
            builder.Services.AddHttpClient(WebApiNameConstants.AppApi, config =>
            {
                config.BaseAddress = new Uri(builder.Configuration["WebApiUrls:Default"] ?? string.Empty);
                
            });
           
            builder.Services.AddDbContext<WebDbContext>(config =>
            {
                config.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            }).AddIdentityCore<IdentityUser>(config =>
            {
                config.Password.RequireLowercase = false;
                config.Password.RequiredLength = 4;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                config.Password.RequireDigit = false;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<WebDbContext>()
            .AddDefaultTokenProviders()
            .AddApiEndpoints();
            
            builder.Services.AddBusinessLogicServices();
            builder.Services.AddControllers(options =>
            {
                //options.
            });
            var apiAddress = builder.Configuration["WebApiUrls:Default"];
#if DEBUG
            apiAddress = "http://localhost:5158";
#endif
            builder.Services.AddApiConsumers(apiAddress, ServiceScope.Scoped);
            builder.Services.AddRazorPages();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.UseRequestLocalization(op => {
                op.AddSupportedCultures("en-za");
                op.SetDefaultCulture("en-za");
            });
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseCors();
          
            app.UseAuthorization();


            app.MapControllers();
            app.MapIdentityApi<IdentityUser>();
            app.MapRazorPages();
          
            app.Run();
        }
    }
}
