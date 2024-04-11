using ChatFrontend.Classes;
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

namespace ChatFrontend.Components
{
    /// <summary>
    /// Логика взаимодействия для MyMessage.xaml
    /// </summary>
    public partial class MyMessage : UserControl
    {
        Classes.Message currentMessage;
        public MyMessage(Classes.Message message)
        {
            InitializeComponent();
            currentMessage = message;
            Load();
        }
        void Load()
        {
            MessageAt.Text = currentMessage.At.ToLocalTime().ToString();
            MessageText.Text = currentMessage.Text;
            MessageFrom.Text = currentMessage.From;
        }
    }
}
