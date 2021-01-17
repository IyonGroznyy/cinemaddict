using Cinemaddict.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Cinemaddict.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _username;
        private string _password;

        public LoginViewModel()
        {
        }

        public string Username
        {
            get => _username;
            set
            {
                if (value == _username) return;
                _username = value.Trim();
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (value == _password) return;
                _password = value.Trim();
                OnPropertyChanged(nameof(Password));
            }
        }
    }
}
