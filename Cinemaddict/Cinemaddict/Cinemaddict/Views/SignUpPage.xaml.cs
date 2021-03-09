using Cinemaddict.Models;
using Cinemaddict.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public SignUpPage()
        {
            Title = "Sign Up";
            InitializeComponent();
        }

        private async void SignUpButton_Clicked(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(EmailEntry.Text.Trim(), @"^([a-zA-Z0-9]+[a-zA-Z0-9\.]*[a-zA-Z0-9]+)@(gmail)\.(com)$", RegexOptions.IgnoreCase))
            {
                await DisplayAlert("Authentication Failed", "Email is incorrect", "OK");
                EmailEntry.Text = "";
                return;
            }
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
            await Navigation.PushAsync(new BIOPage(EmailEntry.Text.Trim(), PasswordEntry.Text.Trim()));
        }

       
    }
}