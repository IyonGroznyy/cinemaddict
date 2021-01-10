using Cinemaddict.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Cinemaddict.ViewModels
{
    public class NewsViewModel : BaseViewModel
    {
        public NewsViewModel()
        {
            Title = "News";
            CategoriesList = new ObservableCollection<Item>
            {
                new Item() { Text = "Data 1", Id =  "1", Description = "sfdadsfasdfasfdasdfasdfasf" },
                new Item() { Text = "Data 2", Id =  "2", Description = "sfdadsfasdfasfdasdfasdfasf" },
                new Item() { Text = "Data 3", Id =  "3", Description = "sfdadsfasdfasfdasdfasdfasf" },
                new Item() { Text = "Data 4", Id =  "4", Description = "sfdadsfasdfasfdasdfasdfasf" },
                new Item() { Text = "Data 5", Id =  "5", Description = "sfdadsfasdfasfdasdfasdfasf" }
            };
        }

        private ObservableCollection<Item> _Categories;
        public ObservableCollection<Item> CategoriesList
        {
            get
            {
                return this._Categories;
            }
            set
            {
                if (_Categories != value)
                {
                    this._Categories = value;
                    SetProperty(ref _Categories, value);
                }
            }
        }
    }
}