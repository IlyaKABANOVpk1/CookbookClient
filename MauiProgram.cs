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
            builder.Services.AddSingleton<CategoryService>();
            builder.Services.AddSingleton<CategoriesViewModel>();
            builder.Services.AddSingleton<AuthStateService>();


            builder.Services.AddSingleton<AuthViewModel>();

            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<RegisterPage>();
            builder.Services.AddSingleton<MainPage>();

            builder.Services.AddTransient<CategoriesPage>();
            builder.Services.AddTransient<CategoryEditPage>();
            builder.Services.AddTransient<IngredientService>();
            builder.Services.AddTransient<IngredientsViewModel>();
            builder.Services.AddTransient<IngredientsPage>();

            builder.Services.AddTransient<IngredientEditViewModel>();
            builder.Services.AddTransient<IngredientEditPage>();
            builder.Services.AddTransient<RecipeService>();
            builder.Services.AddTransient<RecipesViewModel>();
            builder.Services.AddTransient<RecipesPage>();

            builder.Services.AddTransient<RecipeEditViewModel>();
            builder.Services.AddTransient<RecipeEditPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

