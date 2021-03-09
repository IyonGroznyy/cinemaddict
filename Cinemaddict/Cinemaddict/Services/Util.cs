using Cinemaddict.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Cinemaddict.Services
{
    class Util
    {
        public static void SaveDataLocal(User user)
        {
            Preferences.Set("DisplayName", user.DisplayName);
            Preferences.Set("Id", (int)user.Id);
            Preferences.Set("Email", user.Email);
            Preferences.Set("About", user.About);
            Preferences.Set("PhotoUri", user.PhotoUri);
            Preferences.Set("Follwers", user.Follwers.ToStringFromIntList());
            Preferences.Set("Subscriptions", user.Subscriptions.ToStringFromIntList());
            Preferences.Set("Follower_count", (int)user.Follower_count);
            Preferences.Set("Following_count", (int)user.Following_count);
            Preferences.Set("Posts_count", (int)user.Posts_count);
        }
        public static User GetDataLocal()
        {
            return new User()
            {
                DisplayName = Preferences.Get("DisplayName", ""),
                Id = Preferences.Get("Id", 0),
                Email = Preferences.Get("Email", ""),
                About = Preferences.Get("About", ""),
                PhotoUri = Preferences.Get("PhotoUri", ""),
                Follwers = Preferences.Get("Follwers", "").Split(';').ToIntList(),
                Subscriptions = Preferences.Get("Subscriptions", "").Split(';').ToIntList(),
                Follower_count = Preferences.Get("Follower_count", 0),
                Following_count = Preferences.Get("Following_count", 0),
                Posts_count = Preferences.Get("Posts_count", 0)
            };
        }
    }
}
