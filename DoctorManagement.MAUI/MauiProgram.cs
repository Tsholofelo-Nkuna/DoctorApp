using DoctorManagement.Api.Consumer;
using DoctorManagement.MAUI.Services;
using DoctorManagement.MAUI.Services.Interfaces;
using DoctorManagement.Presentation;
using DoctorManagement.Shared.Constants;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace DoctorManagement.MAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");

                });

            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US"); // Example: Spanish (Spain)
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US"); // Example: Spanish (Spain)
            builder.Services.AddMauiBlazorWebView();
            builder.Services.Configure<GeolocationRequest>(config =>
            {
                config.Timeout = TimeSpan.FromSeconds(10);
                config.DesiredAccuracy = GeolocationAccuracy.Medium;
            });
            
            builder.Services.AddSingleton<IAppLocationService, AppLocationService>();
            builder.Services.AddHttpContextAccessor();

            var apiBaseAddress = "https://epichealth.growthlytix.co.za";
#if DEBUG
                apiBaseAddress = "http://localhost:5158";
#endif
            builder.Services.AddPresentationServices(ServiceScope.Singleton, builder.Configuration);
            builder.Services.AddApiConsumers(apiBaseAddress, ServiceScope.Singleton);
            builder.Services.Configure<ApiOptions>(opt =>
            {
                opt.BaseAddress = apiBaseAddress;
                opt.HttpClientName = "DefaultApi";
            });
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
