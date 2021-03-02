using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Cinemaddict.ViewModels;

namespace Cinemaddict.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsDetailPage : ContentPage
    {
        public NewsDetailPage(ItemsDetailViewModel newDetailViewModel)
        {
            InitializeComponent();
            BindingContext = newDetailViewModel;
        }
    }
}