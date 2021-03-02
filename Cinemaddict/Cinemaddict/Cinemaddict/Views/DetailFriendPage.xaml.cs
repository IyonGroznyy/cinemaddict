using Cinemaddict.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cinemaddict.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailFriendPage : ContentPage
    {
        public DetailFriendPage(DetailFriendViewModel detailFriendViewModel)
        {
            InitializeComponent();
            BindingContext = detailFriendViewModel;
        }
    }
}