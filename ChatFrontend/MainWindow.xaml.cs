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

namespace ChatFrontend
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance;
        public static BackendAdapter Adapter = new BackendAdapter();
        public MainWindow()
        {
            InitializeComponent();
            Instance = this;

            OpenPage(new Pages.Register());
        }
        public void OpenPage(Page page)
        {
            frame.Navigate(page);
        }
    }
}
