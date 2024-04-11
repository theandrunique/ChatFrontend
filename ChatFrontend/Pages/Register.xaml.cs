using ChatFrontend.FormBuilder;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

            form.Children.Insert(0, registerForm.Build());
        }
        

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
        }

        private void AuthPage_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Count!");
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
