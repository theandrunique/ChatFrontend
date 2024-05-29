using ChatFrontend.Services;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using ChatFrontend.Views.Windows;
using ShopContent.ViewModels.Base;
using System;
using ChatFrontend.ViewModels.Auth;
using ChatFrontend.Services.Base;
using ChatFrontend.ViewModels;

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
            services.AddSingleton<MainWindowVM>();

            services.AddSingleton<AuthService>();
            services.AddSingleton<JsonService>();
            services.AddSingleton<INavigationService, NavigationService>();

            services.AddSingleton<SignUpVM>();
            services.AddSingleton<LoginVM>();
            

            services.AddSingleton<ViewModels.Messenger.MainVM>();


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
