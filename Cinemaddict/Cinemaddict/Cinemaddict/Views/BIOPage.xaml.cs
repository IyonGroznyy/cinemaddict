using Cinemaddict.Models;
using Cinemaddict.Services;
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
        IFirebaseAuthentication auth = Application.Current.Properties["auth"] as IFirebaseAuthentication;
        public BIOPage(string pEmail, string pPassword)
        {
            email = pEmail;
            password = pPassword;
            InitializeComponent();
            photoUri = "NoAvatar.png";
        }

        private async void ImageButton_Pressed(object sender, EventArgs e)
        {
            (sender as ImageButton).IsEnabled = false;
            await CrossMedia.Current.Initialize();
            try
            {
                file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });
                if (file == null)
                    return;
                image.Source = ImageSource.FromStream(() =>
                {
                    var imageStram = file.GetStream();
                    return imageStram;
                });
                photoUri = await new FirebaseHelper().StoreImages(file.GetStream(), Path.GetFileName(file.Path));
            }
            catch (Exception ex)
            {

            }
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
                var firebase = new FirebaseHelper();
                User user = new User()
                {
                    Id = (await firebase.GetUserCount()),
                    Email = email,
                    Follwers = new List<int>(),
                    Subscriptions = new List<int>() { 0 },
                    About = AboutEntry.Text,
                    DisplayName = NameEntry.Text,
                    PhotoUri = photoUri,
                    Follower_count = 0,
                    Following_count = 1,
                    Posts_count = 0
                };
                await DisplayAlert("Success", "New User Created", "OK");
                await firebase.AddUser(user);
                Util.SaveDataLocal(user);
                await firebase.UpdateUserCount();
                await new FirebaseHelper().UpdateUser(new User() { Follwers = new List<int>() { Preferences.Get("Id", -1) } }, 0);
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                await DisplayAlert("Authentication Failed", "Email or password are incorrect. Try again!", "OK");
            }
        }
    }
}