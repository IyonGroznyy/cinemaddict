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
            await firebase.UpdateUser(
                id: user.Id, 
                email: user.Email, 
                displayName: NameEntry.Text, 
                about: AboutEntry.Text, 
                photoUri: photoUri);
            Application.Current.MainPage = new AppShell();
        }
    }
}