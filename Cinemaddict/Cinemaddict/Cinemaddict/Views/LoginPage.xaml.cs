using Cinemaddict.Models;
using Cinemaddict.Services;
using Cinemaddict.ViewModels;
using System;
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
            Preferences.Clear();
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
            catch (Exception exx)
            {

            }

            if (token != string.Empty)
            {
                Preferences.Set("token", token);
                viewModel.Login();
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