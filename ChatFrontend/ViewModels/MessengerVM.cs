using ChatFrontend.Core;
using ChatFrontend.Models;
using ChatFrontend.Services.Base;
using ShopContent.ViewModels.Base;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ChatFrontend.ViewModels
{
    public class MessengerVM : ViewModel
    {
        private readonly IChatsService _chatsService;

        private ChatVM _selectedChat;
        private string _messageInput;
        private User _currentUser;

        public ObservableCollection<FileItem> Files { get; set; } = new ObservableCollection<FileItem>();

        public IChatsService ChatService
        {
            get => _chatsService;
        }

        public ChatVM SelectedChat
        {
            get => _selectedChat;
            set
            {
                _selectedChat = value;
                OnPropertyChanged(nameof(SelectedChat));
                OnChatSelectedChanged();
            }
        }

        public string MessageInput
        {
            get => _messageInput;
            set
            {
                _messageInput = value;
                OnPropertyChanged(nameof(MessageInput));
            }
        }

        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }

        public ICommand SendMessageCommand { get; set; }
        public ICommand DropCommand { get; set; }
        public ICommand RemoveFileCommand { get; set; }

        public MessengerVM(IChatsService chatService, AppState appState)
        {
            _chatsService = chatService;
            CurrentUser = appState.User;

            SendMessageCommand = new LambdaCommand(ExecuteSendMessageCommand, CanExecuteSendMessage);
            DropCommand = new LambdaCommand(OnDrop);
            RemoveFileCommand = new LambdaCommand(RemoveFile);
        }

        private bool CanExecuteSendMessage(object arg)
        {
            return !string.IsNullOrWhiteSpace(MessageInput);
        }

        private async void ExecuteSendMessageCommand(object obj)
        {
            if (SelectedChat == null)
                return;

            if (SelectedChat.IsNew)
                await ChatService.SendMessage(MessageInput, SelectedChat.Chat.Id);
            else
                await ChatService.SendMessage(MessageInput);

            MessageInput = string.Empty;
        }

        private async void OnChatSelectedChanged()
        {
            if (SelectedChat == null)
                return;
            await ChatService.OpenChat(SelectedChat.Chat);
        }

        public void OpenChatWith(User user)
        {
            var existedChat = ChatService.Chats.FirstOrDefault(c => c.Chat.Id == user.Id);

            if (existedChat != null)
            {
                SelectedChat = existedChat;
            }
            else
            {
                SelectedChat = new ChatVM(new Chat()
                {
                    Id = user.Id,
                    Name = user.Username,
                    ImageUrl = user.ImageUrl,
                    Type = "private",
                })
                {
                    IsNew = true,
                };
            }
        }

        private void OnDrop(object e)
        {

            if ((e as DragEventArgs).Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] droppedFiles = (string[])(e as DragEventArgs).Data.GetData(DataFormats.FileDrop);
                foreach (var file in droppedFiles)
                {
                    Files.Add(new FileItem
                    {
                        FileName = Path.GetFileName(file),
                        FilePath = file,
                        FileSize = new FileInfo(file).Length,
                        //FileIcon = 
                    });
                }
            }
        }

        private void RemoveFile(object file)
        {
            Files.Remove((file as FileItem));
        }
    }
}
