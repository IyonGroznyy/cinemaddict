using Cinemaddict.Models;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinFirebase.Helper;

namespace Cinemaddict.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    [QueryProperty(nameof(NewsItem), nameof(NewsItem))]
    public class ItemsDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string text;
        private string description;
        private string newsItem;
        public int Id { get; set; }

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

        public string NewsItem
        {
            get
            {
                return newsItem;
            }
            set
            {
                newsItem = value;
                LoadItemJson(newsItem);
            }
        }


        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }
        public void LoadItemJson(string itemJson)
        {
            Item item = null;
            try
            {
                Id =int.Parse(itemJson.Split(new char[] { ';'})[0]);
                Text = itemJson.Split(new char[] { ';'})[1];
                Description = itemJson.Split(new char[] { ';'})[2];
            }
            catch(Exception ex)
            {

            }
          
            //Id = item.Id;
            //Text = item.Text;
            //Description = item.Description;
           
        }
        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await new FirebaseHelper().GetPost(int.Parse(itemId));
                Id = item.Id;
                Text = item.Text;
                Description = item.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
