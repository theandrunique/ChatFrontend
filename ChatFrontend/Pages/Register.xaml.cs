using ChatFrontend.Classes;
using ChatFrontend.FormBuilder;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace ChatFrontend.Pages
{
    /// <summary>
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class Register : Page
    {
        InputField username;
        InputField email;
        PasswordField password;
        PasswordField confirmPassword;
        TextBox commonFormError;
        public Register(string login = null)
        {
            InitializeComponent();
            CreateForm();

            if (login != null)
                if (login.Contains("@"))
                    email.Value = login;
                else
                    username.Value = login;
        }
        private void CreateForm()
        {
            var registerForm = new FormBuilder.Form();
            registerForm.AddHeader("Sign up", new Thickness(0, 0, 0, 20));
            username = registerForm.AddInputField("username");
            email = registerForm.AddInputField("email");
            password = registerForm.AddPasswordInputField("password");
            confirmPassword = registerForm.AddPasswordInputField("confirm password");
            commonFormError = registerForm.BuildCommonErrorField();

            form.Children.Insert(0, registerForm.Build());
        }

        private void AuthPage_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void ClearAllErrors()
        {
            username.ClearError();
            email.ClearError();
            password.ClearError();
            confirmPassword.ClearError();
            commonFormError.Text = "";
        }
        private bool CheckIsFieldsValid()
        {
            bool isErrors = false;

            if (string.IsNullOrEmpty(username.Value))
            {
                username.SetError("Required field");
                isErrors = true;
            }
            if (string.IsNullOrEmpty(email.Value))
            {
                email.SetError("Required field");
                isErrors = true;
            }
            if (string.IsNullOrEmpty(password.Value))
            {
                password.SetError("Required field");
                isErrors = true;
            }
            if (password.Value != confirmPassword.Value)
            {
                confirmPassword.SetError("Passwords don't match");
                isErrors = true;
            }
            return isErrors;

        }
        private async void Register_Click(object sender, RoutedEventArgs e)
        {
            ClearAllErrors();
            if (CheckIsFieldsValid()) return;

            RegisterButton.IsEnabled = false;

            var res = await MainWindow.Adapter.SignUp(username.Value, email.Value, password.Value);
            if (!res.IsSuccessStatusCode)
            {
                int statusCode = Convert.ToInt32(res.StatusCode);
                if (statusCode == 422)
                {
                    var json = await res.Content.ReadAsStringAsync();
                    UnprocessableEntity error = JsonConvert.DeserializeObject<UnprocessableEntity>(json);

                    foreach (var detail in error.Detail)
                    {
                        if (detail.Loc[1] == "username")
                            username.SetError(detail.Msg);
                        else if (detail.Loc[1] == "email")
                            email.SetError(detail.Msg);
                        else if (detail.Loc[1] == "password")
                            password.SetError(detail.Msg);
                        else
                            commonFormError.Text = detail.Msg;
                    }
                }
                else if (statusCode >= 400 && statusCode < 500)
                {
                    var json = await res.Content.ReadAsStringAsync();

                    ErrorResponse error = JsonConvert.DeserializeObject<ErrorResponse>(json);

                    if (error.Detail.Contains("username"))
                        username.SetError(error.Detail);
                    else if (error.Detail.Contains("email"))
                        password.SetError(error.Detail);
                    else
                        commonFormError.Text = error.Detail;

                }
                else if (statusCode >= 500 && statusCode < 600)
                {
                    commonFormError.Text = "Sever Error";
                }
            }
            else
            {
                MainWindow.Instance.OpenPage(new MessangerPage());
            }


            RegisterButton.IsEnabled = true;
        }
        private void SetLoading(bool loading)
        {
            if (loading) ButtonLoading.Visibility = Visibility.Visible;
            else ButtonLoading.Visibility = Visibility.Collapsed;
        }

        private void LogIn_Button(object sender, RoutedEventArgs e)
        {
            string transfer = null;
            if (!string.IsNullOrEmpty(username.Value))
                transfer = username.Value;
            if (!string.IsNullOrEmpty(email.Value))
                transfer = email.Value;

            MainWindow.Instance.OpenPage(new Auth(transfer));
        }
    }
}
