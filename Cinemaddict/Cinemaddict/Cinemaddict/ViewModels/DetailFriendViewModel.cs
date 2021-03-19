using Cinemaddict.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinFirebase.Helper;

namespace Cinemaddict.ViewModels
{
    public class DetailFriendViewModel : BaseViewModel
    {
        
        private User friend;

        public Command LoadItemsCommand { get; }

        public Command<Label> LabelItemTapped { get; }

        public User Friend
        {
            get => friend;
            set
            {
                Title = value.DisplayName;
                if (value.PhotoUri == null)
                {
                    value.PhotoUri = "NoAvatar.png";
                }
                SetProperty(ref friend, value);
            }
        }

        public ObservableCollection<Post> Posts { get; }

        public DetailFriendViewModel()
        {
            Posts = new ObservableCollection<Post>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            LabelItemTapped = new Command<Label>(OnLabelTap);
        }


        void OnLabelTap(Label item)
        {
            if (item == null)
                return;
            item.LineBreakMode = LineBreakMode.WordWrap;
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Posts.Clear();
                var items = await new FirebaseHelper().GetAllPosts((int)Friend.Id);

                foreach (var item in items)
                {
                    Posts.Add(item);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
