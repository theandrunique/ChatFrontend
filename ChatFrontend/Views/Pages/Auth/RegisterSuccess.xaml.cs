using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace ChatFrontend.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegisterSuccess.xaml
    /// </summary>
    public partial class RegisterSuccess : Page
    {
        string email;
        TextBox commonError;
        public RegisterSuccess(string email)
        {
            InitializeComponent();
            this.email = email;
            Load();
        }
        private void Load()
        {
            var form = new FormBuilder.Form();
            commonError = form.BuildCommonErrorField();
            MainStack.Children.Add(form.Build());
        }
        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.OpenPage(new Auth(email));
        }

        private async void EmailConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            EmailConfirmButton.IsEnabled = false;
            try
            {
                var res = await MainWindow.Adapter.ConfirmElailSend(email);
            }
            catch (HttpRequestException)
            {
                commonError.Text = "Internet connection error";
                EmailConfirmButton.IsEnabled = true;
                return;
            }

            EmailConfirm.Visibility = Visibility.Collapsed;
            EmailSent.Visibility = Visibility.Visible;
            EmailConfirmButton.IsEnabled = true;
        }
    }
}
