using ChatFrontend.Services;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using ChatFrontend.Views.Windows;
using ShopContent.ViewModels.Base;
using System;
using ChatFrontend.Services.Base;
using ChatFrontend.ViewModels;
using System.Security.Cryptography.Xml;

namespace ChatFrontend
{
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<MainWindow>(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainWindowVM>()
            });

            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<JsonService>();
            services.AddSingleton<INavigationService, NavigationService>();

            services.AddSingleton<MainWindowVM>();
            services.AddTransient<SignUpVM>();
            services.AddTransient<LoginVM>();
            services.AddSingleton<ChatVM>();

            services.AddSingleton<MessengerVM>();
            services.AddTransient<RegisterSuccessVM>();
            services.AddSingleton<NavigationMenuVM>();
            services.AddSingleton<SettingsVM>();

            services.AddSingleton<Func<Type, ViewModel>>(serviceProvider => viewModelType => (ViewModel)serviceProvider.GetRequiredService(viewModelType));

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }
    }
}
