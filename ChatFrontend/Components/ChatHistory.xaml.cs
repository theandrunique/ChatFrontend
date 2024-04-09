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
    /// Логика взаимодействия для ChatHistory.xaml
    /// </summary>
    public partial class ChatHistory : UserControl
    {
        Classes.ChatHistory currentHistory;
        public ChatHistory(Classes.ChatHistory history)
        {
            currentHistory = history;
            InitializeComponent();
            LoadContent();
        }

        void LoadContent()
        {
            HistoryWith.Text = currentHistory.HistoryWith;
            LastMessageText.Text = currentHistory.LastMessage;
            MakeAsNew(currentHistory.HasNew);
        }
        public void MakeAsNew(bool isNew)
        {
            if (isNew)
                IsNew.Text = "New";
        }
    }
}
