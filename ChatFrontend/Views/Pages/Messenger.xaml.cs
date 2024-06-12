using ChatFrontend.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace ChatFrontend.Views.Pages
{
    public partial class Messenger : UserControl
    {
        public Messenger()
        {
            InitializeComponent();
        }

        private void ListView_Drop(object sender, DragEventArgs e)
        {
            if (DataContext is MessengerVM viewModel)
            {
                viewModel.DropCommand.Execute(e);
            }
        }
    }
}
