using LoginBaseApp.Service;
using LoginBaseApp.ViewModels;
using LoginBaseApp.Views;
using Microsoft.Extensions.Logging;

namespace LoginBaseApp
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
                    fonts.AddFont("MaterialSymbolsOutlined.ttf", "MaterialSymbols");
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Pages + VMs => Transient (מופע חדש בכל ניווט)
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<LoginPageViewModel>();

            builder.Services.AddTransient<RegistrationPage>();
            builder.Services.AddTransient<RegistrationPageViewModel>();

            builder.Services.AddTransient<ProfilePage>();
            builder.Services.AddTransient<ProfileViewModel>();

            // Repos/Services/Session => Singleton
            builder.Services.AddSingleton<SQlUserRepository>();
            builder.Services.AddSingleton<ILoginService, SqlLoginService>();
            builder.Services.AddSingleton<IRegisterService, SqlRegisterService>();
            builder.Services.AddSingleton<IUserSession, UserSession>();

            // Pages & VMs (Transient)
            builder.Services.AddTransient<ProductsPage>();
            builder.Services.AddTransient<ProductsPageViewModel>();
            builder.Services.AddTransient<ProductEditPage>();
            builder.Services.AddTransient<ProductEditViewModel>();

            // Repos (Singleton)
            builder.Services.AddSingleton<SqlProductRepository>();
            builder.Services.AddSingleton<SqlOrderRepository>();

            builder.Services.AddTransient<AddProductPage>();
            builder.Services.AddTransient<AddProductViewModel>();

            builder.Services.AddTransient<ProductEditPage>();
            builder.Services.AddTransient<ProductEditViewModel>();


#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}
