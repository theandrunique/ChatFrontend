using System.Collections.Generic;
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

        string loginCurrentValue = "";
        string passwordCurrentValue = "";

        List<Control> uiControls = new List<Control>();

        private Control currentFocusElement;

        bool isButtonDown = false;
        bool isMouseOver = false;
        SolidColorBrush defaultColor = new SolidColorBrush(Color.FromRgb(116, 53, 156));
        SolidColorBrush mouseOverColor = new SolidColorBrush(Color.FromRgb(94, 47, 124));
        SolidColorBrush mouseDownColor = new SolidColorBrush(Color.FromRgb(80, 44, 102));

        SolidColorBrush errorColor = new SolidColorBrush(Color.FromRgb(239, 49, 36));

        SolidColorBrush focusColor = new SolidColorBrush(Color.FromRgb(36, 15, 90));
        Thickness focusThickness = new Thickness(2);

        SolidColorBrush unfocusColor = new SolidColorBrush(Color.FromRgb(26, 26, 26));
        Thickness unfocusThickness = new Thickness(1);

        private void FocusBorder(Border border)
        {
            border.BorderBrush = focusColor;
            border.BorderThickness = focusThickness;
        }
        private void UnFocusBorder(Border border)
        {
            border.BorderBrush = unfocusColor;
            border.BorderThickness = unfocusThickness;
        }
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
            if (currentFocusElement != signInText)
            {
                currentFocusElement = signInText;
                FocusBorder(signIn);
            }
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                isButtonDown = true;
                signIn.Background = mouseDownColor;
            }
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
            SetLoginError("- Some very very long error that cannot be shown in window\n- overflow overflow\n- overflow overflow overflow overflow overflow");
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
            if (login != currentFocusElement)
            {
                loginBorder.BorderBrush = defaultColor;
                loginError.Text = null;
            }
        }
        private void ClearPasswordError()
        {
            if (password != currentFocusElement)
            {
                passwordBorder.BorderBrush = defaultColor;
                passwordError.Text = null;
            }
        }
        private void SetPasswordError(string error)
        {
            passwordError.Text = error;
            passwordBorder.BorderBrush = errorColor;
        }

        private void login_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (!login.Text.Equals(loginCurrentValue))
            {
                loginCurrentValue = login.Text;
                ClearLoginError();
            }
        }

        private void password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!password.Password.Equals(passwordCurrentValue))
            {
                passwordCurrentValue = password.Password;
                ClearPasswordError();   
            }
        }

        private void login_GotFocus(object sender, RoutedEventArgs e)
        {
            if (currentFocusElement != login)
            {
                currentFocusElement = login;
                FocusBorder(loginBorder);
            }
        }

        private void login_LostFocus(object sender, RoutedEventArgs e)
        {
            UnFocusBorder(loginBorder);
        }

        private void password_GotFocus(object sender, RoutedEventArgs e)
        {
            if (currentFocusElement != password)
            {
                currentFocusElement = password;
                FocusBorder(passwordBorder);
            }
        }

        private void password_LostFocus(object sender, RoutedEventArgs e)
        {
            UnFocusBorder(passwordBorder);
        }
    }
}
