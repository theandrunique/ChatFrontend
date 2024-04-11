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
        InputField login;
        public Register()
        {
            InitializeComponent();
            CreateForm();
        }
        private void CreateForm()
        {
            var registerForm = new FormBuilder.Form();
            registerForm.AddHeader("Register", new Thickness(0, 0, 0, 20));
            login = registerForm.AddInputField("login");
            registerForm.AddInputField("password");

            form.Children.Add(registerForm.Build());
        }
        

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
            {
                login.SetError("weads");
            }
            if (e.RightButton == MouseButtonState.Released)
            {
                login.ClearError();
            }
        }

        private void AuthPage_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
