using Cinemaddict.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cinemaddict.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailFriendPage : ContentPage
    {
        DetailFriendViewModel _viewModel;
        public DetailFriendPage(DetailFriendViewModel detailFriendViewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = detailFriendViewModel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private async void btnSubscribers_Clicked(object sender, EventArgs e)
        {
            if (_viewModel.Friend.Follwers != null)
            {
                await Navigation.PushAsync(new DemoFriendsPage(_viewModel.Friend.Follwers));
            }
        }

        private async void btnFollowing_Clicked(object sender, EventArgs e)
        {
            if (_viewModel.Friend.Subscriptions != null)
            {
                await Navigation.PushAsync(new DemoFriendsPage(_viewModel.Friend.Subscriptions));
            }
        }
    }
}