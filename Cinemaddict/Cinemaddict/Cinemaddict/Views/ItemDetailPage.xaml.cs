using Cinemaddict.Models;
using Cinemaddict.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;
using XamarinFirebase.Helper;

namespace Cinemaddict.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage(ItemsDetailViewModel itemsDetailViewModel)
        {
            InitializeComponent();
            BindingContext = itemsDetailViewModel;
        }

        private async void SaveButton_Clicked(object sender, System.EventArgs e)
        {
            await new FirebaseHelper().UpdatePost(new Post() 
            { 
                Id = ((ItemsDetailViewModel)BindingContext).Id,
                TitleText = TitleEditor.Text,
                Description = DescriptionEditor.Text
            });
        }
        
        private void PostImage_Clicked(object sender, System.EventArgs e)
        {
            
        }
    }
}