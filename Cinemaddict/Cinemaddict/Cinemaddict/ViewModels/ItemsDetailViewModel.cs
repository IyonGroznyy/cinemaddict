using Cinemaddict.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Cinemaddict.ViewModels
{
    public class ItemsDetailViewModel : BaseViewModel
    {
        private string text;
        private string description;
        private string uri;
        public int Id { get; set; }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string Uri
        {
            get => uri;
            set => SetProperty(ref uri, value);
        }
    }
}
