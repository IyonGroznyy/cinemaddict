using Cinemaddict.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Cinemaddict.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}