using ChatFrontend.Services.Base;
using ShopContent.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Timers;

namespace ChatFrontend.ViewModels
{
    public class SearchVM : ViewModel
    {
        private readonly IUsersService _usersService;
        private readonly ISessionNavigationService _sessionNavigationService;

        private string _searchQuery;
        private ObservableCollection<UserCardVM> _searchResults = new ObservableCollection<UserCardVM>();
        private Timer _searchDelayTimer;

        public string SearchQuery
        {
            get { return _searchQuery; }
            set
            {
                _searchQuery = value;
                OnPropertyChanged(nameof(SearchQuery));
                StartSearchTimer();
            }
        }
        public ObservableCollection<UserCardVM> SearchResults
        {
            get => _searchResults;
            set
            {
                _searchResults = value;
                OnPropertyChanged(nameof(SearchResults));
            }
        }

        public SearchVM(IUsersService usersService, ISessionNavigationService sessionNavigationService)
        {
            _usersService = usersService;
            _sessionNavigationService = sessionNavigationService;

            _searchDelayTimer = new Timer(300);
            _searchDelayTimer.Elapsed += OnSearchDelayTimerElapsed;
            _searchDelayTimer.AutoReset = false;
        }

        private void StartSearchTimer()
        {
            _searchDelayTimer.Stop();
            _searchDelayTimer.Start();
        }

        private async void OnSearchDelayTimerElapsed(object sender, ElapsedEventArgs e)
        {
            await ExecuteSearch();
        }

        private async Task ExecuteSearch()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                return;
            }

            var users = await _usersService.SearchUsers(SearchQuery);
            if (users != null)
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    SearchResults.Clear();
                    foreach (var user in users.Result)
                    {
                        SearchResults.Add(new UserCardVM(user, _sessionNavigationService));
                    }
                });
            }
        }
    }
}
