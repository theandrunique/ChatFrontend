using ChatFrontend.Core;
using ChatFrontend.Services.Base;
using ChatFrontend.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using ShopContent.ViewModels.Base;
using System;

namespace ChatFrontend.Services
{
    public class SessionServiceProvider : ISessionServiceProvider
    {
        private readonly ServiceProvider _serviceProvider;

        public SessionServiceProvider(AppState appState, IJsonService jsonService, ISessionNavigationService sessionNavigationService, IAuthService authService, Settings settings)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<Settings>(settings);

            services.AddSingleton<IMessengerService, MessengerService>();
            services.AddSingleton<IAuthService>(authService);
            services.AddSingleton<IWebSocketService, WebSocketService>();
            services.AddSingleton<IChatsService, ChatsService>();
            services.AddSingleton<IMessagesService, MessagesService>();
            services.AddSingleton<IStorageService, StorageService>();
            services.AddSingleton<IUsersService, UsersService>();

            services.AddSingleton<AppState>(appState);
            services.AddSingleton<IJsonService>(jsonService);
            services.AddSingleton<ISessionNavigationService>(sessionNavigationService);

            services.AddSingleton<MessengerVM>();
            services.AddSingleton<SettingsVM>();
            services.AddSingleton<SearchVM>();

            _serviceProvider = services.BuildServiceProvider();
        }

        public Func<Type, ViewModel> GetViewModelFactory() => viewModelType => (ViewModel)_serviceProvider.GetRequiredService(viewModelType);
    }
}
