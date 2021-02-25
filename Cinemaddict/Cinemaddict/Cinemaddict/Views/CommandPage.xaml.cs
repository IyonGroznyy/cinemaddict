using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private async void BtnDeleteUser_Clicked(object sender, EventArgs e)
        {
            //try
            //{
            //    await firebase.DeleteUser(int.Parse(DeleteId.Text));
            //}
            //catch(Exception ex)
            //{
            //    await DisplayAlert("Error", $"Некоректный номер \n {ex.Message}", "Заново");
            //}
        }
    }
}