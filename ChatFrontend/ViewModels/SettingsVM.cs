using ChatFrontend.Models;
using ChatFrontend.Services.Base;
using Microsoft.Win32;
using ShopContent.ViewModels.Base;
using System.IO;
using System.Windows.Input;
using ChatFrontend.Core;

namespace ChatFrontend.ViewModels
{
    public class SettingsVM : ViewModel
    {
        private readonly IAuthService _authService;
        private readonly IStorageService _storageService;

        private User _user;

        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        public ICommand ChangeProfilePictureCommand { get; }

        public SettingsVM(IAuthService authService, IStorageService storageService, AppState appState)
        {
            User = appState.User;
            _authService = authService;
            _storageService = storageService;
            ChangeProfilePictureCommand = new LambdaCommand(ExecuteChangeProfilePicture);
        }

        private async void ExecuteChangeProfilePicture(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                string fileName = Path.GetFileName(filePath);

                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    var response = await _storageService.PutFiles(fileStream);
                    User.ImageUrl = response.Files[fileName];

                    await _authService.UpdateImageUrl(response.Files[fileName]);
                    OnPropertyChanged(nameof(User));
                }
            }
        }
    }
}
