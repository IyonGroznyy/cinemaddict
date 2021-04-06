using System;
using System.Text.RegularExpressions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFirebase.Helper;

namespace Cinemaddict.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            Title = "Sign Up";
            InitializeComponent();
            Preferences.Clear();
        }

        private async void SignUpButton_Clicked(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(EmailEntry.Text.Trim(), @"^([a-zA-Z0-9]+[a-zA-Z0-9\.]*[a-zA-Z0-9]+)@(gmail)\.(com)$", RegexOptions.IgnoreCase))
            {
                await DisplayAlert("Authentication Failed", "Email is incorrect", "OK");
                EmailEntry.Text = "";
                return;
            }
#if !DEBUG
            if (!Regex.IsMatch(PasswordEntry.Text.Trim(), @"[0-9]+", RegexOptions.IgnoreCase))
            {
                await DisplayAlert("Authentication Failed", "Password doesn't contain a number", "OK");
                PasswordEntry.Text = "";
                return;
            }
            if (!Regex.IsMatch(PasswordEntry.Text.Trim(), @"[a-z]+", RegexOptions.IgnoreCase))
            {
                await DisplayAlert("Authentication Failed", "Password doesn't contain a letter", "OK");
                PasswordEntry.Text = "";
                return;
            }
            if (!Regex.IsMatch(PasswordEntry.Text.Trim(), @".{8,}", RegexOptions.IgnoreCase))
            {
                await DisplayAlert("Authentication Failed", "Password doesn't contain 8 chars", "OK");
                PasswordEntry.Text = "";
                return;
            }
#endif
            var auth = Application.Current.Properties["auth"] as IFirebaseAuthentication;
            var token = "";
            try
            {
                token = await auth.SignUpWithEmailAndPassword(EmailEntry.Text.Trim(), PasswordEntry.Text.Trim());
                if(string.IsNullOrEmpty(token))
                {
                    throw new Exception();
                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("Authentication Failed", "Email is incorrect", "OK");
                return;
            }
            Preferences.Set("token", token);
            await Navigation.PushAsync(new BIOPage(EmailEntry.Text.Trim(), PasswordEntry.Text.Trim()));
        }

       
    }
}