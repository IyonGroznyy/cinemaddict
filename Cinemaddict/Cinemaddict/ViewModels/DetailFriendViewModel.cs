using Cinemaddict.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Cinemaddict.ViewModels
{
    public class DetailFriendViewModel : BaseViewModel
    {
        private User friend;
        public User Friend
        {
            get => friend;
            set
            {
                Title = value.DisplayName;
                if(value.PhotoUri == null)
                {
                    value.PhotoUri = "NoAvatar.png";
                }
                SetProperty(ref friend, value);
            }
        }
    }
}
