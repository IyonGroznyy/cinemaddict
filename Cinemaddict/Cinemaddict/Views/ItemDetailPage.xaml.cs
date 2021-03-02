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
    }
}