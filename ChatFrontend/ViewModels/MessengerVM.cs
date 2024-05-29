using ShopContent.ViewModels.Base;

namespace ChatFrontend.ViewModels
{
    public class MessengerVM : ViewModel
    {
        public ChatVM ChatVM { get; }

        public MessengerVM(ChatVM chatVM)
        {
            ChatVM = chatVM;
        }
    }
}
