using ShopContent.ViewModels.Base;
using System.Windows;

namespace ChatFrontend.ViewModels.Messenger
{
    public class MainVM : ViewModel
    {
        public ChatVM ChatVM { get; } = new ChatVM();

        public MainVM()
        {
            MessageBox.Show("IM HERE");
        }
    }
}
