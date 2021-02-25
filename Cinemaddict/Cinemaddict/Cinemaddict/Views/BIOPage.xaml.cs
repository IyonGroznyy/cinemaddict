using Cinemaddict.Models;
using Cinemaddict.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
    public partial class BIOPage : ContentPage
    {
        User user;
        string photoUri;
        MediaFile file;
        public BIOPage(User pUser)
        {
            user = pUser;
            InitializeComponent();
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
            //Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
            //if (stream != null)
            //{
            //    photoUri = await new FirebaseHelper().StoreImages(stream, "Avatar");//ImageSource.FromStream(() => stream);
            //    image.Source = photoUri;
            //}
            (sender as ImageButton).IsEnabled = true;
        }

        private async void ReadyButton_Clicked(object sender, EventArgs e)
        {
            var firebase = new FirebaseHelper();
            user.About = AboutEntry.Text;
            user.DisplayName = NameEntry.Text;
            user.PhotoUri = photoUri;
            user.Follwers = new List<int>();
            user.Subscriptions = new List<int>() { 0 };
            user.Follower_count = 0;
            user.Following_count = 1;
            user.Posts_count = 0;
            await firebase.AddUser(user);
            Preferences.Set("DisplayName", user.DisplayName);
            Preferences.Set("Id", user.Id);
            Preferences.Set("Email", user.Email);
            Preferences.Set("About", user.About);
            Preferences.Set("PhotoUri", user.PhotoUri);
            Preferences.Set("Follwers", user.Follwers.ToStringFromIntList());
            Preferences.Set("Subscriptions", user.Subscriptions.ToStringFromIntList());
            Preferences.Set("Follower_count", user.Follower_count);
            Preferences.Set("Following_count", user.Following_count);
            Preferences.Set("Posts_count", user.Posts_count);
            await firebase.UpdateUserCount();
            Application.Current.MainPage = new AppShell();
        }
    }
}