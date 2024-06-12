using ChatFrontend.Services;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ChatFrontend.Views.Windows;
using ShopContent.ViewModels.Base;
using System;
using ChatFrontend.Services.Base;
using ChatFrontend.ViewModels;
using ChatFrontend.Core;

namespace ChatFrontend
{
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();

            IConfigurationRoot config = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json")
                 .Build();

            Settings settings = config.GetRequiredSection("Settings").Get<Settings>();
            services.AddSingleton<Settings>(settings);


            services.AddSingleton<MainWindow>(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainWindowVM>()
            });

            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<IJsonService, JsonService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<ISessionNavigationService, SessionNavigationService>();
            services.AddTransient<ISessionServiceProvider, SessionServiceProvider>();

            services.AddSingleton<MainWindowVM>();

            services.AddTransient<SignUpVM>();
            services.AddTransient<RegisterSuccessVM>();
            services.AddTransient<LoginVM>();
            services.AddTransient<SessionPageVM>();

            services.AddSingleton<AppState>();

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
