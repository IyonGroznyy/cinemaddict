using Cinemaddict.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFirebase.Helper;

namespace Cinemaddict.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        LoginViewModel viewModel;
        IFirebaseAuthentication auth;

        public LoginPage()
        {
            InitializeComponent();
            Title = "Login";
            auth = Application.Current.Properties["auth"] as IFirebaseAuthentication;
            BindingContext = viewModel = new LoginViewModel();
        }

        private async void SignUpButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }
        

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            string token = "";
            try
            {
                token = await auth.LoginWithEmailAndPassword(viewModel.Username, viewModel.Password);
            }
            catch(Exception exx)
            {
                
            }
            
            if (token != string.Empty)
            {
                Preferences.Set("token", token);
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                ShowError();
            }
        }

        private async void ShowError()
        {
            await DisplayAlert("Authentication Failed", "Email or password are incorrect. Try again!", "OK");
        }
    }
}