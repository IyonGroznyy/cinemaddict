using Cinemaddict.Models;
using Cinemaddict.ViewModels;
using Cinemaddict.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cinemaddict.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;
        static ItemsViewModel ItemsViewModel = new ItemsViewModel();
        
        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ItemsViewModel();
            ToolbarItems.Add(tb);
            ToolbarItems.Add(tb2);
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
        ToolbarItem tb2 = new ToolbarItem
        {
            Text = "Chose",
            Order = ToolbarItemOrder.Secondary
        };
    }
}