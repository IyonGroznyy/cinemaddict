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
            await new FirebaseHelper().UpdatePost(((ItemsDetailViewModel)BindingContext).Id, TitleEditor.Text, DescriptionEditor.Text);
        }
    }
}