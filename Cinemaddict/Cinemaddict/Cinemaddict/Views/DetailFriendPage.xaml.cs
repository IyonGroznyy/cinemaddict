using Cinemaddict.Models;
using Cinemaddict.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFirebase.Helper;

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
            //await Navigation.PushAsync(new DemoFriendsPage(new FriendsViewModel(null, null , true)));
        }

        private async void btnFollowing_Clicked(object sender, EventArgs e)
        {
            //if(_viewModel.Friend.Subscriptions!=null)
            //{
            //    var users = new ObservableCollection<User>();
            //    foreach (int id in _viewModel.Friend.Subscriptions)
            //    {
            //        users.Add(await new FirebaseHelper().GetUser(id));
            //    }
            //    await Navigation.PushAsync(new DemoFriendsPage(new FriendsViewModel(null, users, true)));
            //}
        }
    }
}