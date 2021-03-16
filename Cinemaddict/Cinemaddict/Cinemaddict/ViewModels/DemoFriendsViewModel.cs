using Cinemaddict.Models;
using Cinemaddict.Services;
using Cinemaddict.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinFirebase.Helper;

namespace Cinemaddict.ViewModels
{
    public class DemoFriendsViewModel : BaseViewModel
    {
        private User _selectedUser;
        private User _currentUser;
        private List<int> _showUserIds;
        public ObservableCollection<LocalUser> Users { get; }
        public Command LoadUserCommand { get; }
        public Command AddUserCommand { get; }
        public Command<Tuple<int, int>> SubUserCommand { get; }
        public Command<User> UserTapped { get; }
        public INavigation Navigation { set; get; }
        public ObservableCollection<Color> ButtonSubCol { set; get; }
        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                SetProperty(ref _selectedUser, value);
                OnUserSelected(value);
            }
        }
        public DemoFriendsViewModel(INavigation pNavigation, List<int> showUserIds)
        {
            Title = "My Friends";
            _showUserIds = showUserIds;
            GetCurrentUser();
            Navigation = pNavigation;
            Users = new ObservableCollection<LocalUser>();
            ButtonSubCol = new ObservableCollection<Color>();
            LoadUserCommand = new Command(async () => await ExecuteLoadUsersCommand());
            UserTapped = new Command<User>(OnUserSelected);
            SubUserCommand = new Command<Tuple<int, int>>(OnSubscribeUser);
            AddUserCommand = new Command(OnAddUser);
            if (pNavigation != null)
            {
                var existingPages = pNavigation.NavigationStack.ToList();

                foreach (var page in existingPages)
                {
                    Navigation.RemovePage(page);
                }
            }
                
        }

        async void SubButtonsRefresh()
        {
            var tempList = new ObservableCollection<Color>();
            foreach (var user in Users)
            {
                if(_currentUser == null)
                {
                    await GetCurrentUserAsync();
                }
                if (_currentUser.Subscriptions.Exists(x => x == (int)user.Id))
                {
                    tempList.Add(Color.Gray);
                }
                else
                {
                    tempList.Add(Color.Blue);
                }
            }
            ButtonSubCol = tempList;
        }

        async Task ExecuteLoadUsersCommand()
        {
            IsBusy = true;

            try
            {
                var usersDB = new List<User>();
                foreach (int id in _showUserIds)
                {
                    usersDB.Add(await new FirebaseHelper().GetUser(id));
                }
                 
                lock (Users)
                {
                    Users.Clear();
                    int i = 0;
                    foreach (var user in usersDB)
                    {
                        Users.Add(new LocalUser(user, i));
                        i++;
                    }
                    SubButtonsRefresh();
                }
            }
            catch (Exception ex)
            {
 
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedUser = null;
        }

        private async Task GetCurrentUserAsync()
        {
            _currentUser = await new FirebaseHelper().GetCurrentUser();
        }

        private void GetCurrentUser()
        {
            _currentUser = Util.GetDataLocal();
        }



        private async void OnAddUser(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        private async void OnSubscribeUser(Tuple<int, int> id)
        {
            await new FirebaseHelper().UpdateUser(new User() { Subscriptions = new List<int>() { id.Item1 } });
            await new FirebaseHelper().UpdateUser(new User() { Follwers = new List<int>() { Preferences.Get("Id", -1) } }, id.Item1);
            await GetCurrentUserAsync();
            await ExecuteLoadUsersCommand();
        }

        async void OnUserSelected(User user)
        {
            if (user == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Navigation.PushAsync(new DetailFriendPage(new DetailFriendViewModel() { Friend = user }));
        }
    }
}
