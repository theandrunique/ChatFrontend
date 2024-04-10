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
        public Register()
        {
            InitializeComponent();
            CreateForm();
        }
        private void CreateForm()
        {
            StackPanel stackPanelForm = new StackPanel();
            stackPanelForm.Orientation = Orientation.Vertical;
            stackPanelForm.Children.Add(FormBuilder.FormBuilder.CreateFormHeader(
                "Register",
                new Thickness(0, 0, 0, 20)
                ));
            var inputField = new FormBuilder.InputField();
            inputField.AddPlaceholder("keklol");
            stackPanelForm.Children.Add(inputField.Build());

            var secondInputField = new FormBuilder.InputField();
            secondInputField.AddPlaceholder("keklol123");
            stackPanelForm.Children.Add(secondInputField.Build());

            var passwordField =  new FormBuilder.PasswordField();
            passwordField.AddPlaceholder("password");
            stackPanelForm.Children.Add(passwordField.Build());

            form.Children.Add(stackPanelForm);
        }
        

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void AuthPage_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
