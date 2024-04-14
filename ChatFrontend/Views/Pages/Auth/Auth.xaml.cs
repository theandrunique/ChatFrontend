using ChatFrontend.Classes;
using ChatFrontend.FormBuilder;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChatFrontend.Pages
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Page
    {
        InputField login;
        PasswordField password;
        TextBox commonFormError;
        public Auth(string login = null)
        {
            InitializeComponent();
            CreateForm();

            if (login != null)
                this.login.Value = login;
        }
        private void CreateForm()
        {
            var registerForm = new FormBuilder.Form();
            registerForm.AddHeader("Log in", new Thickness(0, 0, 0, 20));
            login = registerForm.AddInputField("login");
            password = registerForm.AddPasswordInputField("password");
            commonFormError = registerForm.BuildCommonErrorField();

            form.Children.Insert(0, registerForm.Build());
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LogIn_Click(null, null);
            }
        }

        private void AuthPage_Loaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            window.KeyDown += Window_KeyDown;
        }

        private async void LogIn_Click(object sender, RoutedEventArgs e)
        {
            login.ClearError();
            password.ClearError();
            commonFormError.Text = "";

            bool isErrors = false;

            if (string.IsNullOrEmpty(login.Value))
            {
                login.SetError("Required field");
                isErrors = true;
            }
            if (string.IsNullOrEmpty(password.Value))
            {
                password.SetError("Required field");
                isErrors = true;
            }

            if (isErrors) return;

            LogInButton.IsEnabled = false;
            try
            {
                var res = await MainWindow.Adapter.GetAuth(login.Value, password.Value);
                if (!res.IsSuccessStatusCode)
                {
                    int statusCode = Convert.ToInt32(res.StatusCode);
                    if (statusCode == 422)
                    {
                        var json = await res.Content.ReadAsStringAsync();
                        UnprocessableEntity error = JsonConvert.DeserializeObject<UnprocessableEntity>(json);

                        StringBuilder errorMessage = new StringBuilder();
                        foreach (var detail in error.Detail)
                        {
                            errorMessage.AppendLine($"{detail.Loc[1]} | {detail.Msg}");
                        }
                        commonFormError.Text = errorMessage.ToString();
                    }
                    else if (statusCode >= 400 && statusCode < 500)
                    {
                        var json = await res.Content.ReadAsStringAsync();

                        ErrorResponse error = JsonConvert.DeserializeObject<ErrorResponse>(json);

                        login.SetError(error.Detail);
                        password.SetError(error.Detail);
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
            }
            catch (HttpRequestException)
            {
                commonFormError.Text = "Internet connection error";
            }

            LogInButton.IsEnabled = true;
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.OpenPage(new Register(login.Value));
        }
    }
}
