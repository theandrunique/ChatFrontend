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

namespace ChatFrontend.Pages
{
    /// <summary>
    /// Логика взаимодействия для MessangerPage.xaml
    /// </summary>
    public partial class MessangerPage : Page
    {
        InputField messageText;
        public MessangerPage()
        {
            InitializeComponent();
            CreateMessageInput();
            Load();
        }
        void CreateMessageInput()
        {
            var messageInput = new Form();
            messageText = messageInput.AddInputField("Write a message...", false, 700, 700, 200, 16, true, 10);
            MessageInput.Children.Insert(0, messageInput.Build());
        }
        void Load()
        {
            var message = new Classes.Message();
            message.At = DateTime.Now;
            message.Text = "Hello!SAKLDHJoekasyhffuijFFEAHDfvuiofesdyhffjuioersfdufjfseiodfufskdojfsdoijflsdjfjjjjjjjjfjsdjfdsjklfdjklsdsfjlksdfjkldfsjklsdfjjklsdfjsdfklfjlkjlkdssdfjklsdfjklsdjklfsdjfkljdfksjklsdfjklsdfjklsdfjklsdfjklsdfjklsdfjklsdjklsdf";
            message.From = "theandru";

            var uiMessage = new Components.Message(message);
            uiMessage.HorizontalAlignment = HorizontalAlignment.Left;

            MessagesList.Children.Add(uiMessage);

            message.Text = "Hello!";

            var uiMessage1 = new Components.Message(message);
            uiMessage1.HorizontalAlignment = HorizontalAlignment.Left;
            uiMessage1.Margin = new Thickness(0, 10, 0, 0);
            MessagesList.Children.Add(uiMessage1);

            var myMessage = new Components.MyMessage(message);
            myMessage.HorizontalAlignment = HorizontalAlignment.Right;
            myMessage.Margin = new Thickness(0, 10, 0, 0);

            MessagesList.Children.Add(myMessage);

        }
    }
}
