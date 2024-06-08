using ShopContent.ViewModels.Base;
using System;

namespace ChatFrontend.Services.Base
{
    public interface ISessionServiceProvider
    {
        Func<Type, ViewModel> GetViewModelFactory();
    }
}
