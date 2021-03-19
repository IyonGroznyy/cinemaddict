using Cinemaddict.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cinemaddict.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FriendsPage : ContentPage
    {
        FriendsViewModel _viewModel;
        //static FriendsViewModel FriendsViewModel = new FriendsViewModel(null);
        public FriendsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new FriendsViewModel(Navigation);
            //ToolbarItems.Add(tb);
            //ToolbarItems.Add(tb2);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        //ToolbarItem tb = new ToolbarItem
        //{

        //    Text = "ADD",
        //    Order = ToolbarItemOrder.Secondary,
        //    Command = FriendsViewModel.AddUserCommand
        //};
        //ToolbarItem tb2 = new ToolbarItem
        //{
        //    Text = "Choose",
        //    Order = ToolbarItemOrder.Secondary
        //};
    }
}