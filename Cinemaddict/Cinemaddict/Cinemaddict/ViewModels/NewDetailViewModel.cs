using Cinemaddict.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cinemaddict.ViewModels
{
    public class NewDetailViewModel : BaseViewModel
    {
        private string text;
        private string description;
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
    }
}
