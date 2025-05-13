using DoctorManagement.Api.Consumer;
using DoctorManagement.MAUI.Services;
using DoctorManagement.MAUI.Services.Interfaces;
using DoctorManagement.Presentation;
using DoctorManagement.Shared.Constants;
using Microsoft.Extensions.Logging;

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

            builder.Services.AddMauiBlazorWebView();
            builder.Services.Configure<GeolocationRequest>(config =>
            {
                config.Timeout = TimeSpan.FromSeconds(10);
                config.DesiredAccuracy = GeolocationAccuracy.Medium;
            });
            builder.Services.AddSingleton<IAppLocationService, AppLocationService>();


            var apiBaseAddress = "https://epichealth.growthlytix.co.za";
#if DEBUG
                apiBaseAddress = "http://localhost:5158";
#endif
            builder.Services.AddPresentationServices(ServiceScope.Singleton, builder.Configuration);
            builder.Services.AddApiConsumers(apiBaseAddress, ServiceScope.Singleton);
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
