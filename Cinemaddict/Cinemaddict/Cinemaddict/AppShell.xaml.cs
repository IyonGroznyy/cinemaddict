using Cinemaddict.ViewModels;
using Cinemaddict.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XamarinFirebase.Helper;

namespace Cinemaddict
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            var auth = Application.Current.Properties["auth"] as IFirebaseAuthentication;
            auth.SignOut();
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
