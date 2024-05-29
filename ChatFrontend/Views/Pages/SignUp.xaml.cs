using System.Windows.Controls;

namespace ChatFrontend.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class SignUp : UserControl
    {
        public SignUp()
        {
            InitializeComponent();
        }

        //private bool CheckIsFieldsValid()
        //{
        //    bool isErrors = false;

        //    if (string.IsNullOrEmpty(username.Value))
        //    {
        //        username.SetError("Required field");
        //        isErrors = true;
        //    }
        //    if (string.IsNullOrEmpty(email.Value))
        //    {
        //        email.SetError("Required field");
        //        isErrors = true;
        //    }
        //    if (string.IsNullOrEmpty(password.Value))
        //    {
        //        password.SetError("Required field");
        //        isErrors = true;
        //    }
        //    if (password.Value != confirmPassword.Value)
        //    {
        //        confirmPassword.SetError("Passwords don't match");
        //        isErrors = true;
        //    }
        //    return isErrors;
        //}
    //    private async void Register_Click(object sender, RoutedEventArgs e)
    //    {
    //        ClearAllErrors();
    //        if (CheckIsFieldsValid()) return;

    //        RegisterButton.IsEnabled = false;
    //        try
    //        {
    //            var res = await MainWindow.Adapter.SignUp(username.Value, email.Value, password.Value);
    //            if (!res.IsSuccessStatusCode)
    //            {
    //                int statusCode = Convert.ToInt32(res.StatusCode);
    //                if (statusCode == 422)
    //                {
    //                    var json = await res.Content.ReadAsStringAsync();
    //                    UnprocessableEntity error = JsonConvert.DeserializeObject<UnprocessableEntity>(json);

    //                    foreach (var detail in error.Detail)
    //                    {
    //                        if (detail.Loc[1] == "username")
    //                            username.SetError(detail.Msg);
    //                        else if (detail.Loc[1] == "email")
    //                            email.SetError(detail.Msg);
    //                        else if (detail.Loc[1] == "password")
    //                            password.SetError(detail.Msg);
    //                        else
    //                            commonFormError.Text = detail.Msg;
    //                    }
    //                }
    //                else if (statusCode >= 400 && statusCode < 500)
    //                {
    //                    var json = await res.Content.ReadAsStringAsync();

    //                    ErrorResponse error = JsonConvert.DeserializeObject<ErrorResponse>(json);

    //                    if (error.Detail.Contains("username"))
    //                        username.SetError(error.Detail);
    //                    else if (error.Detail.Contains("email"))
    //                        email.SetError(error.Detail);
    //                    else
    //                        commonFormError.Text = error.Detail;

    //                }
    //                else if (statusCode >= 500 && statusCode < 600)
    //                {
    //                    commonFormError.Text = "Sever Error";
    //                }
    //            }
    //            else
    //            {
    //                MainWindow.Instance.OpenPage(new RegisterSuccess(email.Value));
    //            }
    //        }
    //        catch (HttpRequestException)
    //        {
    //            commonFormError.Text = "Internet connection error";
    //        }

    //        RegisterButton.IsEnabled = true;
    //    }
    //    private void SetLoading(bool loading)
    //    {
    //        if (loading) ButtonLoading.Visibility = Visibility.Visible;
    //        else ButtonLoading.Visibility = Visibility.Collapsed;
    //    }

    //    private void LogIn_Button(object sender, RoutedEventArgs e)
    //    {
    //        string transfer = null;
    //        if (!string.IsNullOrEmpty(username.Value))
    //            transfer = username.Value;
    //        if (!string.IsNullOrEmpty(email.Value))
    //            transfer = email.Value;

    //        MainWindow.Instance.OpenPage(new Auth(transfer));
    //    }
    }
}
