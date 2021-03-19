using Cinemaddict.ViewModels;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cinemaddict.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DemoFriendsPage : ContentPage
    {
        internal DemoFriendsViewModel _viewModel;
        //static DemoFriendsViewModel FriendsViewModel = new DemoFriendsViewModel(null,null);
        public DemoFriendsPage(List<int> usersIds)
        {
            InitializeComponent();
            BindingContext = _viewModel = new DemoFriendsViewModel(Navigation,usersIds);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}