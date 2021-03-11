﻿using Cinemaddict.Models;
using Cinemaddict.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinFirebase.Helper;
using System.Linq;
using System.Collections.Generic;
using Cinemaddict.Services;
using System.Collections.Specialized;
using Xamarin.Essentials;

namespace Cinemaddict.ViewModels
{
    class FriendsViewModel : BaseViewModel
    {

        private User _selectedUser;
        private User _currentUser;
        public ObservableCollection<User> Users { get; }
        public Command LoadUserCommand { get; }
        public Command AddUserCommand { get; }
        public Command<int> SubUserCommand { get; }
        public Command<User> UserTapped { get; }
        public INavigation Navigation { set; get; }
        public ObservableCollection<Color> ButtonSubCol { set; get; }
        public FriendsViewModel(INavigation pNavigation)
        {
            Title = "My Friends";
            GetCurrentUser();
            Navigation = pNavigation;
            Users = new ObservableCollection<User>();
            ButtonSubCol = new ObservableCollection<Color>();
            LoadUserCommand = new Command(async () => await ExecuteLoadUsersCommand());
            UserTapped = new Command<User>(OnUserSelected);
            SubUserCommand = new Command<int>(OnSubscribeUser);
            AddUserCommand = new Command(OnAddUser);
        }

        void SubButtonsRefresh()
        {
            var tempList = new ObservableCollection<Color>();
            foreach (var user in Users)
            {
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
                var usersDB = await new FirebaseHelper().GetAllUsers();
                lock (Users)
                {
                    Users.Clear();
                    foreach (var user in usersDB)
                    {
                        Users.Add(user);
                    }
                    SubButtonsRefresh();
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

        private async Task GetCurrentUserAsync()
        {
            _currentUser = await new FirebaseHelper().GetCurrentUser();
        }

        private async void GetCurrentUser()
        {
            _currentUser = await new FirebaseHelper().GetCurrentUser();
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

        private async void OnSubscribeUser(int id)
        {
            await new FirebaseHelper().UpdateUser(new User() { Subscriptions = new List<int>() { id } });
            await new FirebaseHelper().UpdateUser(new User() { Follwers = new List<int>() { Preferences.Get("Id", -1) } }, id);
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
