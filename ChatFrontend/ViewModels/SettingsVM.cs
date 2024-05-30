using ShopContent.ViewModels.Base;

namespace ChatFrontend.ViewModels
{
    public class SettingsVM : ViewModel
    {
        public NavigationMenuVM NavigationMenuVM { get; }

        public SettingsVM(NavigationMenuVM navigationMenuVM)
        {
            NavigationMenuVM = navigationMenuVM;
        }
    }
}
