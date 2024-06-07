using ChatFrontend.Core;
using ChatFrontend.Models;
using ChatFrontend.Services;
using ChatFrontend.Services.Base;
using ShopContent.Commands;
using ShopContent.ViewModels.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Input;

namespace ChatFrontend.ViewModels
{
    public class LoginVM : ViewModel
    {
        private readonly AuthenticationState _authenticationState;
        private readonly INavigationService _navigation;
        private readonly IAuthService _authService;
        public ICommand NavigateToSignUpCommand { get; }
        LogInSchema _logInModel = new LogInSchema();

        string _loginError = "";
        string _passwordError = "";

        string _commonError = "";

        public string Login
        {
            get => _logInModel.Login; 
            set
            {
                _logInModel.Login = value;
                OnPropertyChanged(nameof(Login));
                ValidateField(nameof(Login));
            }
        }
        public string Password
        {
            get => _logInModel.Password; set
            {
                _logInModel.Password = value;
                OnPropertyChanged(nameof(Password));
                ValidateField(nameof(Password));
            }
        }
        public string CommonError
        {
            get => _commonError; 
            set
            {
                _commonError = value;
                OnPropertyChanged(nameof(CommonError));
            }
        }
        public string LoginError
        {
            get => _loginError;
            set
            {
                _loginError = value;
                OnPropertyChanged(nameof(LoginError));
            }
        }
        public string PasswordError
        {
            get => _passwordError; set
            {
                _passwordError = value;
                OnPropertyChanged(nameof(PasswordError));
            }
        }
        public ICommand LoginCommand { get; }

        public LoginVM(INavigationService navigation, IAuthService authService, AuthenticationState authenticationState)
        {
            _authenticationState = authenticationState;
            _navigation = navigation;
            _authService = authService;
            LoginCommand = new LambdaCommand(ExecuteLogin, CanExecuteLogin);
            NavigateToSignUpCommand = new LambdaCommand(NavigateToSignUp);
        }

        private bool CanExecuteLogin(object arg)
        {
            return ValidateModel();
        }

        private void NavigateToSignUp(object obj)
        {
            _navigation.NavigateTo<SignUpVM>();
        }
        private async void ExecuteLogin(object obj)
        {
            CommonError = string.Empty;
            try
            {
                await _authService.Login(Login, Password);

                _authenticationState.IsAuthenticated = true;

                _authenticationState.AccessToken = await _authService.Token();

                

                _navigation.NavigateTo<MessengerVM>();
            }
            catch (ErrorResponse ex)
            {
                CommonError = ex.Detail;
            }
        }
        private bool ValidateField(string propertyName)
        {
            var context = new ValidationContext(_logInModel) { MemberName = propertyName };
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateProperty(
                typeof(LogInSchema).GetProperty(propertyName).GetValue(_logInModel),
                context,
                results
            );

            switch (propertyName)
            {
                case nameof(_logInModel.Login):
                    LoginError = results.FirstOrDefault()?.ErrorMessage ?? "";
                    OnPropertyChanged(nameof(LoginError));
                    break;
                case nameof(_logInModel.Password):
                    PasswordError = results.FirstOrDefault()?.ErrorMessage ?? "";
                    OnPropertyChanged(nameof(PasswordError));
                    break;
            }

            return isValid;
        }
        private bool ValidateModel()
        {
            var context = new ValidationContext(_logInModel);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(_logInModel, context, results, true);

            LoginError = "";
            PasswordError = "";
            foreach (var error in results)
            {
                switch (error.MemberNames.FirstOrDefault())
                {
                    case nameof(LogInSchema.Login):
                        LoginError = error.ErrorMessage;
                        break;
                    case nameof(LogInSchema.Password):
                        PasswordError = error.ErrorMessage;
                        break;
                }
            }

            return isValid;
        }
    }
}
