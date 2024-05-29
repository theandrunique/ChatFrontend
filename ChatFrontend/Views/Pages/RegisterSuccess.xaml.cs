using System.Windows.Controls;

namespace ChatFrontend.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegisterSuccess.xaml
    /// </summary>
    public partial class RegisterSuccess : UserControl
    {
        public RegisterSuccess()
        {
            InitializeComponent();
        }

        //private async void EmailConfirmButton_Click(object sender, RoutedEventArgs e)
        //{
        //    EmailConfirmButton.IsEnabled = false;
        //    try
        //    {
        //        var res = await MainWindow.Adapter.ConfirmElailSend(email);
        //    }
        //    catch (HttpRequestException)
        //    {
        //        commonError.Text = "Internet connection error";
        //        EmailConfirmButton.IsEnabled = true;
        //        return;
        //    }

        //    EmailConfirm.Visibility = Visibility.Collapsed;
        //    EmailSent.Visibility = Visibility.Visible;
        //    EmailConfirmButton.IsEnabled = true;
        //}
    }
}
