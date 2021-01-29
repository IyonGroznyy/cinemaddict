using Cinemaddict.Models;
using Cinemaddict.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinFirebase.Helper;

namespace Cinemaddict.ViewModels
{
    class NewsItemsViewModel : BaseViewModel
    {
        private Item _selectedItem;
        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command<Item> ItemTapped { get; }
        public INavigation Navigation { set; get; }
        public NewsItemsViewModel(INavigation pNavigation)
        {
            Title = "News";
            Navigation = pNavigation;
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<Item>(OnItemSelected);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await new FirebaseHelper().GetAllNewsPosts();
                //await new FirebaseHelper().GetAllUsers();
                foreach (var item in items)
                {
                    Items.Add(item);
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

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;
            // This will push the ItemDetailPage onto the navigation stack
            await Navigation.PushAsync(new NewsDetailPage(new NewDetailViewModel() { Description = item.Description, Text = item.Text }));
        }
    }
}
