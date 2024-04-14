using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChatFrontend.ViewModels.Auth
{
    public class MainVM
    {
        LoginVM loginVM;
        SignUpVM signUpVM;
        Page _currentPage;
        public ICommand NavigateToLoginCommand { get; }
        public ICommand NavigateToSignUpCommand { get; }

        public Page CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                _currentPage = value;
            }
        }
        public MainVM()
        {

        }
    }
}
