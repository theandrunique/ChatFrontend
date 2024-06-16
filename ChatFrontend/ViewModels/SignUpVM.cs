using ChatFrontend.Core;
using ChatFrontend.Models;
using ChatFrontend.Services.Base;
using ShopContent.ViewModels.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Input;

namespace ChatFrontend.ViewModels
{
    public class SignUpVM : ViewModel
    {
        private readonly INavigationService _navigation;
        private readonly IAuthService _authService;

        private SignUpSchema _signUpModel = new SignUpSchema();
        private string _commonError = "";
        private string _confirmPassword = "";
        private string _confirmPasswordError = "";

        public SignUpSchema SignUpModel
        {
            get => _signUpModel;
            set
            {
                _signUpModel = value;
                OnPropertyChanged(nameof(SignUpModel));
                ValidateModel();
            }
        }

        public string Username
        {
            get => _signUpModel.Username;
            set
            {
                if (_signUpModel.Username != value)
                {
                    _signUpModel.Username = value;
                    OnPropertyChanged(nameof(Username));
                    ValidateField(nameof(Username));
                }
            }
        }

        public string Email
        {
            get => _signUpModel.Email;
            set
            {
                if (_signUpModel.Email != value)
                {
                    _signUpModel.Email = value;
                    OnPropertyChanged(nameof(Email));
                    ValidateField(nameof(Email));
                }
            }
        }

        public string Password
        {
            get => _signUpModel.Password;
            set
            {
                if (_signUpModel.Password != value)
                {
                    _signUpModel.Password = value;
                    ConfirmPasswordError = "";
                    OnPropertyChanged(nameof(Password));
                    ValidateField(nameof(Password));
                }
            }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                if (_confirmPassword != value)
                {
                    _confirmPassword = value;
                    ConfirmPasswordError = "";
                    if (_signUpModel.Password != _confirmPassword)
                    {
                        ConfirmPasswordError = "Passwords don't match";
                    }
                    OnPropertyChanged(nameof(ConfirmPassword));
                }
            }
        }

        public string UsernameError { get; private set; }
        public string EmailError { get; private set; }
        public string PasswordError { get; private set; }
        public string ConfirmPasswordError
        {
            get => _confirmPasswordError;
            set
            {
                _confirmPasswordError = value;
                OnPropertyChanged(nameof(ConfirmPasswordError));
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

        public ICommand SignUpCommand { get; }
        public ICommand NavigateToLoginCommand { get; }

        public SignUpVM(INavigationService navigation, IAuthService authService)
        {
            _navigation = navigation;
            _authService = authService;
            SignUpCommand = new LambdaCommand(ExecuteSignUp, CanExecuteSignUp);
            NavigateToLoginCommand = new LambdaCommand(NavigateToLogin);
        }

        private void NavigateToLogin(object obj)
        {
            _navigation.NavigateTo<LoginVM>();
        }

        private bool CanExecuteSignUp(object arg)
        {
            return ValidateModel();
        }

        private bool ValidateModel()
        {
            var context = new ValidationContext(SignUpModel);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(SignUpModel, context, results, true);

            UsernameError = "";
            EmailError = "";
            PasswordError = "";
            ConfirmPasswordError = "";

            foreach (var error in results)
            {
                switch (error.MemberNames.FirstOrDefault())
                {
                    case nameof(SignUpModel.Username):
                        UsernameError = error.ErrorMessage;
                        break;
                    case nameof(SignUpModel.Email):
                        EmailError = error.ErrorMessage;
                        break;
                    case nameof(SignUpModel.Password):
                        PasswordError = error.ErrorMessage;
                        break;
                }
            }

            if (_signUpModel.Password != ConfirmPassword)
            {
                ConfirmPasswordError = "Passwords don't match";
                isValid = false;
            }
            OnPropertyChanged(nameof(UsernameError));
            OnPropertyChanged(nameof(EmailError));
            OnPropertyChanged(nameof(PasswordError));
            OnPropertyChanged(nameof(ConfirmPasswordError));

            return isValid;
        }
        private bool ValidateField(string propertyName)
        {
            var context = new ValidationContext(SignUpModel) { MemberName = propertyName };
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateProperty(
                typeof(SignUpSchema).GetProperty(propertyName).GetValue(SignUpModel),
                context,
                results
            );

            switch (propertyName)
            {
                case nameof(SignUpModel.Username):
                    UsernameError = results.FirstOrDefault()?.ErrorMessage ?? "";
                    OnPropertyChanged(nameof(UsernameError));
                    break;
                case nameof(SignUpModel.Email):
                    EmailError = results.FirstOrDefault()?.ErrorMessage ?? "";
                    OnPropertyChanged(nameof(EmailError));
                    break;
                case nameof(SignUpModel.Password):
                    PasswordError = results.FirstOrDefault()?.ErrorMessage ?? "";
                    OnPropertyChanged(nameof(PasswordError));
                    break;
            }

            return isValid;
        }

        private async void ExecuteSignUp(object obj)
        {
            CommonError = string.Empty;

            try
            {
                var success = await _authService.SignUp(Username, Email, Password);
                if (success)
                    _navigation.NavigateTo<RegisterSuccessVM>();
            }
            catch (ErrorResponse ex)
            {
                CommonError = ex.Detail;
            }
        }
    }
}
