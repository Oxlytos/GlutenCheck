using GlutenCheckApp.Interfaces;
using GlutenCheckApp.Services;
using GlutenCheckApp.ViewModels;
using Infrastructure.Gluten.Facades;
using Infrastructure.Gluten.Interfaces;
using Infrastructure.Gluten.Services;
using Microsoft.Extensions.Logging;
using ZXing.Net.Maui.Controls;

namespace GlutenCheckApp
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .UseBarcodeReader();
                ;


            builder.Services.AddSingleton<StandardViewModel>();
            builder.Services.AddSingleton<MainFetchFacade>();


           
            builder.Services.AddSingleton<IJsonStorageService, JsonService>();
            builder.Services.AddSingleton<IMAUIStorageDirectoryHelper,  MAUIStorageDirectoryHelper>();
            builder.Services.AddSingleton<IAccountService, AccountService>();
            builder.Services.AddSingleton<IPhotoRepo, PhotoRepo>();
            builder.Services.AddSingleton<ICameraService, CameraService>(); 
            builder.Services.AddSingleton<IAllergenParser, AllergenParser>();
            builder.Services.AddSingleton<ICameraService, CameraService>();
            builder.Services.AddSingleton<IProductFetcher, ProductFetcher>();
            builder.Services.AddSingleton(new HttpClient());

            return builder.Build();
        }
    }
}
