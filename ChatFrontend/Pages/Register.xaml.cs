using ChatFrontend.FormBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

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
        public Register()
        {
            InitializeComponent();
            CreateForm();
        }
        private void CreateForm()
        {
            var registerForm = new FormBuilder.Form();
            registerForm.AddHeader("Register", new Thickness(0, 0, 0, 20));
            username = registerForm.AddInputField("login");
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
    }
}
