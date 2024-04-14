using ChatFrontend.Classes;
using System.Windows;
using System.Windows.Controls;


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

            OpenPage(new Pages.RegisterSuccess("24w"));
        }
        public void OpenPage(Page page)
        {
            frame.Navigate(page);
        }
    }
}
