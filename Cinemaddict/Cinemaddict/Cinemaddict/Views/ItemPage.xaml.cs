﻿using Cinemaddict.ViewModels;
using Xamarin.Forms;

namespace Cinemaddict.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;
        static ItemsViewModel ItemsViewModel = new ItemsViewModel(null);

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ItemsViewModel(Navigation);
            ToolbarItems.Add(tb);
            //ToolbarItems.Add(tb2);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }


        ToolbarItem tb = new ToolbarItem
        {

            Text = "ADD",
            Order = ToolbarItemOrder.Secondary,
            Command = ItemsViewModel.AddItemCommand
        };
        //ToolbarItem tb2 = new ToolbarItem
        //{
        //    Title = "Choose",
        //    Order = ToolbarItemOrder.Secondary
        //};
    }
}