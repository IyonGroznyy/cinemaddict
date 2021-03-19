using Cinemaddict.Models;
using Cinemaddict.Services;
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
            CurrentUser = Util.GetDataLocal();
            if (string.IsNullOrEmpty(CurrentUser.PhotoUri))
            {
                CurrentUser.PhotoUri = "NoAvatar.png";
            }
            BindingContext = CurrentUser;
            Items.Add(new FlyoutItem
            {
                Title = "News",
                Icon = "icon_feed.png",
                Items =
                {
                    new Tab
                    {
                        Items = { new ShellContent {Content = new NewsPage()} }
                    }
                }
            });
            Items.Add(new FlyoutItem
            {
                Title = "Your Posts",
                Icon = "icon_feed.png",
                Items =
                {
                    new Tab
                    {
                        Items = { new ShellContent {Content = new ItemsPage()} }
                    }
                }
            });
            Items.Add(new FlyoutItem
            {
                Title = "FriendsPage",
                Icon = "icon_feed.png",
                Items =
                {
                    new Tab
                    {
                        Items = { new ShellContent {Content = new FriendsPage()} }
                    }
                }
            });
#if DEBUG
            Items.Add(new FlyoutItem
            {
                Title = "Command Page",
                Icon = "icon_feed.png",
                Items =
                {
                    new Tab
                    {
                        Items = { new ShellContent {Content = new CommandPage()} }
                    }
                }
            });
#endif
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
