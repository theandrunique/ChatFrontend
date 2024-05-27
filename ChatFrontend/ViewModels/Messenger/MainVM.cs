using ShopContent.ViewModels.Base;

namespace ChatFrontend.ViewModels.Messenger
{
    public class MainVM : ViewModel
    {
        public ChatVM ChatVM { get; } = new ChatVM();

        public MainVM()
        {
            
        }
    }
}
