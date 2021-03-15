using Cinemaddict.Models;
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
    public partial class DemoFriendsPage : ContentPage
    {
        internal FriendsViewModel _viewModel;
        static FriendsViewModel FriendsViewModel = new FriendsViewModel(null);
        public DemoFriendsPage(FriendsViewModel friendsViewModel)
        {
            InitializeComponent();
            friendsViewModel.Navigation = Navigation;
            BindingContext = _viewModel = friendsViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}