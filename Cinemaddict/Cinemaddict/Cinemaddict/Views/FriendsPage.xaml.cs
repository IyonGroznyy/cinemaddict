using Cinemaddict.ViewModels;

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

        //    Title = "ADD",
        //    Order = ToolbarItemOrder.Secondary,
        //    Command = FriendsViewModel.AddUserCommand
        //};
        //ToolbarItem tb2 = new ToolbarItem
        //{
        //    Title = "Choose",
        //    Order = ToolbarItemOrder.Secondary
        //};
    }
}