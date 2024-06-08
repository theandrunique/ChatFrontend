﻿using ChatFrontend.Core;
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

        public SessionServiceProvider(AppState appState, IJsonService jsonService)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<IMessengerService, MessengerService>();
            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<IChatService, ChatService>();

            services.AddSingleton<AppState>(provider => appState);
            services.AddSingleton<IJsonService>(provider => jsonService);

            services.AddSingleton<MessengerVM>();
            services.AddSingleton<SettingsVM>();
            services.AddSingleton<SearchVM>();

            _serviceProvider = services.BuildServiceProvider();
        }

        public Func<Type, ViewModel> GetViewModelFactory() => viewModelType => (ViewModel)_serviceProvider.GetRequiredService(viewModelType);
    }
}