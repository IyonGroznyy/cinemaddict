using Cinemaddict.Models;
using Cinemaddict.ViewModels;
using Cinemaddict.Views;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinFirebase.Helper;

namespace Cinemaddict
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        User CurrentUser;
        public AppShell()
        {
            CurrentUser = new User()
            {
                DisplayName = Preferences.Get("DisplayName",""),
                Id = Preferences.Get("Id",0),
                Email = Preferences.Get("Email",""),
                About = Preferences.Get("About",""),
                PhotoUri = Preferences.Get("PhotoUri","")
            };
            BindingContext = CurrentUser;
            InitializeComponent(); 
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(FriendsPage), typeof(FriendsPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            var auth = Application.Current.Properties["auth"] as IFirebaseAuthentication;
            auth.SignOut();
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
