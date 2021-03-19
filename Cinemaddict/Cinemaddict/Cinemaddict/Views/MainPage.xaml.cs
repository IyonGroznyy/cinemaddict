using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFirebase.Helper;

namespace Cinemaddict.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        IFirebaseAuthentication auth;

        public MainPage()
        {
            InitializeComponent();
            auth = Application.Current.Properties["auth"] as IFirebaseAuthentication;
        }

        private void SignOutButton_Clicked(object sender, EventArgs e)
        {
            var signedOut = auth.SignOut();
            if (signedOut)
            {
                Application.Current.MainPage = new LoginPage();
            }
        }
    }
}