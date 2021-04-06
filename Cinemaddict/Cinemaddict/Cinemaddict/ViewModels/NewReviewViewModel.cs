using Cinemaddict.Models;
using Cinemaddict.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.IO;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinFirebase.Helper;

namespace Cinemaddict.ViewModels
{
    public class NewReviewViewModel : BaseViewModel
    {
        MediaFile file;
        private string title;
        private string description;
        private string uri;
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public NewReviewViewModel()
        {
            Uri = "NewPost.png";
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(title)
                && !String.IsNullOrWhiteSpace(description);
        }

        public string TitleText
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }
        public string Uri
        {
            get => uri;
            set => SetProperty(ref uri, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        public async void ImageButtonClick(object sender, ImageButton imageButton)
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
                imageButton.Source = ImageSource.FromStream(() =>
                {
                    var imageStram = file.GetStream();
                    return imageStram;
                });
                Post newItem = new Post()
                {
                    Id = Preferences.Get("Posts_count", 0),
                    TitleText = TitleText,
                    Description = Description
                };
                Uri = (await Post.StoreImages(Path.GetFileName(file.Path), UserOrPost.Post, newItem, null, file.GetStream())).First();
            }
            catch (Exception ex)
            {

            }
            (sender as ImageButton).IsEnabled = true;
        }

        private async void OnSave()
        {
            Post newItem = new Post()
            {
                Id = Preferences.Get("Posts_count", 0),
                TitleText = TitleText,
                Description = Description,
                Uri = Uri
            };
            await firebaseHelper.AddPost(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
