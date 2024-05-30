using ShopContent.ViewModels.Base;

namespace ChatFrontend.ViewModels
{
    public class MessengerVM : ViewModel
    {
        public ChatVM ChatVM { get; }
        public NavigationMenuVM NavigationMenuVM { get; }

        public MessengerVM(ChatVM chatVM, NavigationMenuVM navigationMenuVM)
        {
            ChatVM = chatVM;
            NavigationMenuVM = navigationMenuVM;
        }
    }
}
