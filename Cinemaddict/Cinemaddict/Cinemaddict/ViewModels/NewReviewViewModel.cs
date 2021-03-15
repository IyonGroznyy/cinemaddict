using Cinemaddict.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinFirebase.Helper;

namespace Cinemaddict.ViewModels
{
    public class NewReviewViewModel : BaseViewModel
    {
        MediaFile file;
        private string text;
        private string description;
        private string uri;
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public NewReviewViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(text)
                && !String.IsNullOrWhiteSpace(description);
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
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
                Uri = await new FirebaseHelper().StoreImages(file.GetStream(), Path.GetFileName(file.Path));
            }
            catch (Exception ex)
            {

            }
            (sender as ImageButton).IsEnabled = true;
        }

        private async void OnSave()
        {
            Item newItem = new Item()
            {
                Id = Preferences.Get("Posts_count", 0),
                Text = Text,
                Description = Description,
                Uri = Uri
            };
            await firebaseHelper.AddPost(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
