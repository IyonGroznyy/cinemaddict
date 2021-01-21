using Cinemaddict.Models;
using Cinemaddict.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFirebase.Helper;

namespace Cinemaddict.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        IFirebaseAuthentication auth = Application.Current.Properties["auth"] as IFirebaseAuthentication;
        public SignUpPage()
        {
            Title = "Sign Up";
            InitializeComponent();
        }

        private async void SignUpButton_Clicked(object sender, EventArgs e)
        {
            string token = "";
            try
            {
                var signOut = auth.SignOut();
                token = await auth.SignUpWithEmailAndPassword(EmailEntry.Text.Trim(), PasswordEntry.Text.Trim());
            }
            catch (Exception exx)
            {

            }

            if (token != string.Empty)
            {
                Preferences.Set("token", token);
                var firebase = new FirebaseHelper();
                User user = new User()
                {
                    Id = (await firebase.GetAllUsers()).Count,
                    Email = EmailEntry.Text.Trim()
                };
                await DisplayAlert("Success", "New User Created", "OK");
                await Navigation.PushAsync(new BIOPage(user));
            }
            else
            {
                ShowError();
            }
        }

        private async void ShowError()
        {
            await DisplayAlert("Authentication Failed", "Email or password are incorrect. Try again!", "OK");
        }
    }
}