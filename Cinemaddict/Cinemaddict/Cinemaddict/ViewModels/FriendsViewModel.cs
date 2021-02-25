using Cinemaddict.Models;
using Cinemaddict.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinFirebase.Helper;
using System.Linq;

namespace Cinemaddict.ViewModels
{
    class FriendsViewModel : BaseViewModel
    {

        private User _selectedUser;
        public ObservableCollection<User> Users { get; }
        public Command LoadUserCommand { get; }
        public Command AddUserCommand { get; }
        public Command<int> DelUserCommand { get; }
        public Command<User> UserTapped { get; }

        public FriendsViewModel()
        {
            Title = "My Friends";
            Users = new ObservableCollection<User>();
            LoadUserCommand = new Command(async () => await ExecuteLoadUsersCommand());
            UserTapped = new Command<User>(OnUserSelected);

            DelUserCommand = new Command<int>(OnDelUser);

            AddUserCommand = new Command(OnAddUser);
        }

        async Task ExecuteLoadUsersCommand()
        {
            IsBusy = true;

            try
            {
                Users.Clear();
                var usersDB = await new FirebaseHelper().GetAllUsers();
                foreach(var user in usersDB)
                {
                    Users.Add(user);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
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

        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                SetProperty(ref _selectedUser, value);
                OnUserSelected(value);
            }
        }

        private async void OnAddUser(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        private async void OnDelUser(int id)
        {
            Users.Remove(Users.Where(x => x.Id == id).FirstOrDefault());
            await new FirebaseHelper().DeletePost(id);
        }
        async void OnUserSelected(User user)
        {
            if (user == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemsDetailViewModel.ItemId)}={user.Id}");
        }
    }
}
