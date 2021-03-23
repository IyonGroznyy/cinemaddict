using Cinemaddict.ViewModels;
using Xamarin.Forms;


namespace Cinemaddict.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        ItemsDetailViewModel viewModel;
        public ItemDetailPage(ItemsDetailViewModel itemsDetailViewModel)
        {
            InitializeComponent();
            BindingContext = viewModel = itemsDetailViewModel;
        }

        private async void SaveButton_Clicked(object sender, System.EventArgs e)
        {
            await viewModel.SavePost(TitleEditor.Text, DescriptionEditor.Text);
        }
        
        private void PostImage_Clicked(object sender, System.EventArgs e)
        {
            
        }
    }
}