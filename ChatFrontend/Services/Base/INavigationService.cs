using ShopContent.ViewModels.Base;

namespace ChatFrontend.Services.Base
{
    public interface INavigationService
    {
        ViewModel CurrentView { get; }
        void NavigateTo<T>() where T : ViewModel;
    }
}
