using System.Windows.Controls;

namespace ChatFrontend.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
        }

        //private async void LogIn_Click(object sender, RoutedEventArgs e)
        //{
        //    login.ClearError();
        //    password.ClearError();
        //    commonFormError.Text = "";

        //    bool isErrors = false;

        //    if (string.IsNullOrEmpty(login.Value))
        //    {
        //        login.SetError("Required field");
        //        isErrors = true;
        //    }
        //    if (string.IsNullOrEmpty(password.Value))
        //    {
        //        password.SetError("Required field");
        //        isErrors = true;
        //    }

        //    if (isErrors) return;

        //    LogInButton.IsEnabled = false;
        //    try
        //    {
        //        var res = await MainWindow.Adapter.GetAuth(login.Value, password.Value);
        //        if (!res.IsSuccessStatusCode)
        //        {
        //            int statusCode = Convert.ToInt32(res.StatusCode);
        //            if (statusCode == 422)
        //            {
        //                var json = await res.Content.ReadAsStringAsync();
        //                UnprocessableEntity error = JsonConvert.DeserializeObject<UnprocessableEntity>(json);

        //                StringBuilder errorMessage = new StringBuilder();
        //                foreach (var detail in error.Detail)
        //                {
        //                    errorMessage.AppendLine($"{detail.Loc[1]} | {detail.Msg}");
        //                }
        //                commonFormError.Text = errorMessage.ToString();
        //            }
        //            else if (statusCode >= 400 && statusCode < 500)
        //            {
        //                var json = await res.Content.ReadAsStringAsync();

        //                ErrorResponse error = JsonConvert.DeserializeObject<ErrorResponse>(json);

        //                login.SetError(error.Detail);
        //                password.SetError(error.Detail);
        //            }
        //            else if (statusCode >= 500 && statusCode < 600)
        //            {
        //                commonFormError.Text = "Sever Error";
        //            }
        //        }
        //        else
        //        {
        //            MainWindow.Instance.OpenPage(new MessangerPage());
        //        }
        //    }
        //    catch (HttpRequestException)
        //    {
        //        commonFormError.Text = "Internet connection error";
        //    }

        //    LogInButton.IsEnabled = true;
        //}

        //private void SignUp_Click(object sender, RoutedEventArgs e)
        //{
        //    MainWindow.Instance.OpenPage(new Register(login.Value));
        //}
    }
}
