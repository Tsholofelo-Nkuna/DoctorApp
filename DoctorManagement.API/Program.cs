
using DoctorManagement.BusinessLogicLayer;
using DoctorManagement.DataAccessLayer;
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
            builder.Services.AddDbContext<WebDbContext>(config =>
            {
                config.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            }).AddIdentity<IdentityUser, IdentityRole>()
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

            app.UseAuthorization();


            app.MapControllers();
            app.MapIdentityApi<IdentityUser>();
            app.Run();
        }
    }
}
