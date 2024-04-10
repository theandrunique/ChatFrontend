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
        public Auth()
        {
            InitializeComponent();
        }
        bool isButtonDown = false;
        bool isMouseOver = false;
        SolidColorBrush defaultColor = new SolidColorBrush(Color.FromRgb(116, 53, 156));
        SolidColorBrush mouseOverColor = new SolidColorBrush(Color.FromRgb(94, 47, 124));
        SolidColorBrush mouseDownColor = new SolidColorBrush(Color.FromRgb(80, 44, 102));

        SolidColorBrush errorColor = new SolidColorBrush(Color.FromRgb(239, 49, 36));

        private void SignIn_MouseEnter(object sender, MouseEventArgs e)
        {
            isMouseOver = true;
            if (!isButtonDown)
                signIn.Background = mouseOverColor;
        }
        private void SignIn_MouseLeave(object sender, MouseEventArgs e)
        {
            isMouseOver = false;
            if (!isButtonDown)
                signIn.Background = defaultColor;
        }

        private void SignIn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isButtonDown = true;
            signIn.Background = mouseDownColor;
        }
        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            SignIn_MouseUp(null, null);
        }
        private void SignIn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!isButtonDown)
                return;

            isButtonDown = false;
            if (isMouseOver)
                signIn.Background = mouseOverColor;
            else
                signIn.Background = defaultColor;

            if (isMouseOver)
            {
                ClickSignIn();
            }
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ClickSignIn();
            }
        }
        private void ClickSignIn()
        {
            string loginValue = login.Text;
            if (string.IsNullOrEmpty(loginValue))
                SetLoginError("Login cannot be empty!");
            string passwordValue = password.Password;
            if (string.IsNullOrEmpty(passwordValue))
                SetPasswordError("Password cannot be empty!");

            MessageBox.Show("Click counted!");
            SetPasswordError("- Some very very long error that cannot be shown in window\n- overflow overflow\n- overflow overflow overflow overflow overflow");
        }

        private void AuthPage_Loaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            window.KeyDown += Window_KeyDown;
        }
        private void SetLoginError(string error)
        {
            loginError.Text = error;
            loginBorder.BorderBrush = errorColor;
        }
        private void ClearLoginError()
        {
            loginBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            loginError.Text = null;
        }
        private void ClearPasswordError()
        {
            passwordBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            passwordError.Text = null;
        }
        private void SetPasswordError(string error)
        {
            passwordError.Text = error;
            passwordBorder.BorderBrush = errorColor;
        }

        private void login_SelectionChanged(object sender, RoutedEventArgs e)
        {
            ClearLoginError();
        }

        private void password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ClearPasswordError();
        }
    }
}
