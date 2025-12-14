using CookbookClient.Services;
using CookbookClient.ViewModel;
using Microsoft.Extensions.Logging;

namespace CookbookClient
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
                });

            builder.Services.AddSingleton<ApiClient>();
            builder.Services.AddSingleton<AuthService>();

            builder.Services.AddSingleton<AuthViewModel>();

            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<RegisterPage>();
            builder.Services.AddSingleton<MainPage>();

            builder.Services.AddTransient<CategoriesPage>();
            builder.Services.AddTransient<IngredientsPage>();
            builder.Services.AddTransient<RecipesPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

