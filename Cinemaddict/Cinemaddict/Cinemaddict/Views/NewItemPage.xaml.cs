using Cinemaddict.ViewModels;
using System;
using Xamarin.Forms;

namespace Cinemaddict.Views
{
    public partial class NewItemPage : ContentPage
    {
        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewReviewViewModel();
        }

        private void PostImage_Clicked(object sender, EventArgs e)
        {
            ((NewReviewViewModel)BindingContext).ImageButtonClick(sender, PostImage);
        }
    }
}