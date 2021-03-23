using Cinemaddict.Models;
using Cinemaddict.Services;
using Cinemaddict.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFirebase.Helper;

namespace Cinemaddict.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BIOPage : ContentPage
    {
        string email;
        string password;
        string photoUri;
        MediaFile file;
        BIOViewModel viewModel = new BIOViewModel();
        IFirebaseAuthentication auth = Application.Current.Properties["auth"] as IFirebaseAuthentication;
        public BIOPage(string pEmail, string pPassword)
        {
            InitializeComponent();
            viewModel.email = pEmail;
            viewModel.password = pPassword;
            viewModel.photoUri = "NoAvatar.png";
            viewModel.AlertNotify += async (string title, string message, string cancel) => await DisplayAlert(title, message, cancel);
        }

        private async void ImageButton_Pressed(object sender, EventArgs e)
        {
            (sender as ImageButton).IsEnabled = false;
            await viewModel.ImagePick(image);
            (sender as ImageButton).IsEnabled = true;
        }

        private async void ReadyButton_Clicked(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(NameEntry.Text) || string.IsNullOrEmpty(AboutEntry.Text))
            {
                await DisplayAlert("Authentication Failed", "Name or About Title are incorrect. Try again!", "OK");
                return;
            }
            string token = "";
            try
            {
                var signOut = auth.SignOut();
                token = await auth.SignUpWithEmailAndPassword(email, password);
            }
            catch (Exception exx)
            {
                await Navigation.PopAsync();
            }

            if (token != string.Empty)
            {
                Preferences.Set("token", token);
                await viewModel.CreateUser(AboutEntry.Text, NameEntry.Text);
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                await DisplayAlert("Authentication Failed", "Email or password are incorrect. Try again!", "OK");
            }
        }
    }
}