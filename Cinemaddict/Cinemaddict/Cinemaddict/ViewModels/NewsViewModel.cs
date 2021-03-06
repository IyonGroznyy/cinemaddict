﻿using Cinemaddict.Models;
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
            CategoriesList = new ObservableCollection<Post>();
        }

        private ObservableCollection<Post> _Categories;
        public ObservableCollection<Post> CategoriesList
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