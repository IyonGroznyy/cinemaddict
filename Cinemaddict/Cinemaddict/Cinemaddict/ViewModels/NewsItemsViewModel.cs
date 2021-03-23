using Cinemaddict.Models;
using Cinemaddict.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinFirebase.Helper;

namespace Cinemaddict.ViewModels
{
    class NewsItemsViewModel : BaseViewModel
    {
        private LocalPost _selectedItem;
        public ObservableCollection<LocalPost> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command<Post> ItemTapped { get; }
        public Command<Tuple<int, Tuple<int, List<int>>>> LikeCommand { get; }
        public INavigation Navigation { set; get; }
        public Command<Label> LabelItemTapped { get; }
        string _likeImage;
        public string LikeImage 
        { 
            set
            {
                SetProperty(ref _likeImage, value);
            } 
            get => _likeImage; 
        }
        string _likeLabel;
        public string LikeLabel
        {
            set
            {
                SetProperty(ref _likeLabel, value);
            }
            get => _likeLabel;
        }
        public NewsItemsViewModel(INavigation pNavigation)
        {
            Title = "News";
            Navigation = pNavigation;
            LikeImage = "DisLike.png";
            Items = new ObservableCollection<LocalPost>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<Post>(OnItemSelected);
            LabelItemTapped = new Command<Label>(OnLabelTap);
            LikeCommand = new Command<Tuple<int, Tuple<int, List<int>>>>(OnLikeTap);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                var items = await Post.GetAllNewsPosts();
                lock (Items)
                {
                    Items.Clear();
                    foreach (var item in items)
                    {
                        Items.Add(item);
                    }
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
            SelectedItem = null;
        }

        public LocalPost SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        void OnLabelTap(Label item)
        {
            if (item == null)
                return;
            item.LineBreakMode = LineBreakMode.WordWrap;
        }

        async void OnLikeTap(Tuple<int, Tuple<int, List<int>>> likeInfo)
        {
            int currentId = Preferences.Get("Id", -1);
            var owners = likeInfo?.Item2?.Item2;
            int likesCount = 0;
            if (owners == null)
            {
                owners = new List<int>();
                likesCount = 0;
            }
            else
            {
                likesCount = likeInfo.Item2.Item1;
            }
            Tuple<int, List<int>> likesAndOwners;
            if (owners.Contains(currentId))
            {
                owners.Remove(currentId);
                likesAndOwners = new Tuple<int, List<int>>(likesCount - 1, owners);
            }
            else
            {
                owners.Add(currentId);
                likesAndOwners = new Tuple<int, List<int>>(likesCount + 1, owners);
            }       
            await new FirebaseHelper().UpdatePost(
                new LocalPost(
                            new Post() 
                            { 
                                LikesAndOwners = likesAndOwners
                            }) 
                { 
                    AuthorId = likeInfo.Item1 
                });
            await ExecuteLoadItemsCommand();
        }

        async void OnItemSelected(Post item)
        {
            if (item == null)
                return;
            // This will push the ItemDetailPage onto the navigation stack
            await Navigation.PushAsync(new NewsDetailPage(new ItemsDetailViewModel() { Description = item.Description, TitleText = item.TitleText, Uri = item.Uri}));
        }
    }
}
