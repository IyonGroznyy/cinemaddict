using Cinemaddict.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cinemaddict.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CommandPage : ContentPage
    {
        public CommandPage()
        {
            InitializeComponent();
        }

        private async void BtnReset_Clicked(object sender, EventArgs e)
        {
            await new CommandViewModel().ResetDB();
        }
    }
}