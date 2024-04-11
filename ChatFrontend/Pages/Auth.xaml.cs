using ChatFrontend.FormBuilder;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChatFrontend.Pages
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Page
    {
        InputField login;
        PasswordField password;
        public Auth()
        {
            InitializeComponent();
            CreateForm();
        }
        private void CreateForm()
        {
            var registerForm = new FormBuilder.Form();
            registerForm.AddHeader("Log In", new Thickness(0, 0, 0, 20));
            login = registerForm.AddInputField("login");
            password = registerForm.AddPasswordInputField("password");

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

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            login.ClearError();
            password.ClearError();

            if (string.IsNullOrEmpty(login.Value))
                login.SetError("Login cannot be empty!");
            if (string.IsNullOrEmpty(password.Value))
                password.SetError("Password cannot be empty!");

            MessageBox.Show("Click counted!");
            login.SetError("- Some very very long error that cannot be shown in window\n- overflow overflow\n- overflow overflow overflow overflow overflow");
            password.SetError("- Some very very long error that cannot be shown in window\n- overflow overflow\n- overflow overflow overflow overflow overflow");
        }
    }
}
