using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFirebase.Helper;

namespace Cinemaddict.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CommandPage : ContentPage
    {
        FirebaseHelper firebase = new FirebaseHelper();
        public CommandPage()
        {
            InitializeComponent();
        }

        private async void BtnReset_Clicked(object sender, EventArgs e)
        {
            await firebase.DeleteAllUser();
        }
    }
}