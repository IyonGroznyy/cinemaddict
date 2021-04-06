using Cinemaddict.Models;
using Cinemaddict.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFirebase.Helper;

namespace Cinemaddict.ViewModels
{
    class BIOViewModel
    {
        public string email;
        public string password;
        public string photoUri;
        public MediaFile file;
        public delegate Task AlertHandler(string title, string message, string cancel);
        public event AlertHandler AlertNotify;

        public async Task ImagePick(ImageButton imageButton)
        {
            await CrossMedia.Current.Initialize();
            try
            {
                file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });
                if (file == null)
                    return;
                imageButton.Source = ImageSource.FromStream(() =>
                {
                    var imageStram = file.GetStream();
                    return imageStram;
                });
                photoUri = (await Post.StoreImages(Path.GetFileName(file.Path), UserOrPost.User, null , new User() { Id = (await User.GetUserCount()) } ,file.GetStream())).First();
            }
            catch (Exception ex)
            {
                AlertNotify?.Invoke("Image pick Failed", "Failed to pick picture. Try again!", "OK");
            }
        }
        public async Task CreateUser(string pAboutEntry, string pNameEntry)
        {
            User user = new User()
            {
                Id = (await User.GetUserCount()),
                Email = email,
                Follwers = new List<int>(),
                Subscriptions = new List<int>() { 0 },
                About = pAboutEntry,
                DisplayName = pNameEntry,
                PhotoUri = photoUri,
                Follower_count = 0,
                Following_count = 1,
                Posts_count = 0
            };
            AlertNotify?.Invoke("Success", "New User Created", "OK");
            await User.AddUser(user);
            Util.SaveDataLocal(user);
            await User.UpdateUserCount();
            await User.UpdateUser(new User() { Follwers = new List<int>() { (int)user.Id } }, 0); // Подписываем на главного юзера
        }
    }
}
