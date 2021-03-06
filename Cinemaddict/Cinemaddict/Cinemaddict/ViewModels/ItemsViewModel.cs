﻿using Cinemaddict.Models;
using Cinemaddict.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinFirebase.Helper;
using System.Linq;
using Xamarin.Essentials;

namespace Cinemaddict.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Post _selectedItem;
        public ObservableCollection<Post> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<int> DelItemCommand { get; }
        public Command<Post> ItemTapped { get; }
        public INavigation Navigation { set; get; }

        public ItemsViewModel(INavigation pNavigation)
        {
            Title = "My posts";
            Navigation = pNavigation;
            Items = new ObservableCollection<Post>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<Post>(OnItemSelected);

            DelItemCommand = new Command<int>(OnDelItem);

            AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await Post.GetAllPosts(Preferences.Get("Id", 0));
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
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

        public Post SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
            await ExecuteLoadItemsCommand();
        }

        private async void OnDelItem(int id)
        {
            Items.Remove(Items.Where(x => x.Id == id).FirstOrDefault());
            await Post.DeletePost(id);
        }
        async void OnItemSelected(Post item)
        {
            if (item == null)
                return;
            await Navigation.PushAsync(new ItemDetailPage(new ItemsDetailViewModel() 
            { 
                Description = item.Description, 
                TitleText = item.TitleText, 
                Uri = item.Uri
            }));
            // This will push the ItemDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemsDetailViewModel.ItemId)}={item.Id}");
        }
    }
}