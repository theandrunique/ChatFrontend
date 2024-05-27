using ShopContent.Commands;
using ShopContent.ViewModels.Base;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChatFrontend.ViewModels.Auth
{
    public class MainVM : ViewModel
    {
        public LoginVM LoginVM { get; } = new LoginVM();
        public SignUpVM SignUpVM { get; } = new SignUpVM();
        UserControl _currentView;
        public ICommand NavigateToLoginCommand { get; }
        public ICommand NavigateToSignUpCommand { get; }

        public UserControl CurrentView
        {
            get
            {
                return _currentView;
            }
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }
        public MainVM()
        {
            NavigateToLoginCommand = new LambdaCommand(NavigateToLogin);
            NavigateToSignUpCommand = new LambdaCommand(NavigateToSignUp);

            CurrentView = new Views.UserControls.Auth();
        }

        private void NavigateToLogin(object obj)
        {
            CurrentView = new Views.UserControls.Auth();
        }

        private void NavigateToSignUp(object obj)
        {
            CurrentView = new Views.UserControls.Register();
        }
    }
}
