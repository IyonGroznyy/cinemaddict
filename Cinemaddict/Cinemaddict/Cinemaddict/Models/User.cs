using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cinemaddict.Models
{
    public class User
    {
        public int Id { get; set; }
        public int Posts_count { get; set; } = 0;
        public int Follower_count { get; set; } = 0;
        public int Following_count { get; set; } = 0;
        public List<int> Follwers { get; set; }
        public List<int> Subscriptions { get; set; }
        public string DisplayName { get; set; }
        public string PhotoUri { get; set; }
        public string Email { get; set; }
        public string About { get; set; }

        public User()
        {

        }

        public User(FirebaseObject<User> item)
        {
            DisplayName = item.Object.DisplayName;
            Id = item.Object.Id;
            About = item.Object.About;
            Email = item.Object.Email;
            PhotoUri = item.Object.PhotoUri;
            if(item.Object.Follwers == null)
            {
                Follwers = new List<int>();
            }
            else
            {
                Follwers = item.Object.Follwers;
            }
            Subscriptions = item.Object.Subscriptions;
            Follower_count = item.Object.Follower_count;
            Following_count = item.Object.Following_count;
            Posts_count = item.Object.Posts_count;
        }

        
        public User(User user)
        {
            List<object> firstObjs = user.GetType().GetFields().Select(x => x.GetValue(user)).ToList();
            int i = 0;
            foreach (var current in GetType().GetFields())
            {
                current.SetValue(this, firstObjs[i]);
                i++;
            }
        }

        public void CopyAndReplace(User user)
        {
            List<object> firstObjs = user.GetType().GetFields().Select(x => x.GetValue(user)).ToList();
            int i = 0;
            foreach (var current in GetType().GetFields())
            {
                if (current.GetValue(current) == null)
                {
                    current.SetValue(this, firstObjs[i]);
                }
                i++;
            }
        }
    }
}
