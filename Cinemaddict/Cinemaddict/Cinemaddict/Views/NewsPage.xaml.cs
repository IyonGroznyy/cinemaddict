using Cinemaddict.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cinemaddict.Views
{
    public partial class NewsPage : ContentPage
    {
        NewsItemsViewModel _viewModel;
        static NewsItemsViewModel ItemsViewModel = new NewsItemsViewModel();
        public NewsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new NewsItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}