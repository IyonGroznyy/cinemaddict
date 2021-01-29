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
            }
        }
    }
}
