using System;

namespace Cinemaddict.Models
{
    public class LocalUser : User
    {
        Tuple<int,int> _localIdAndId;
        /// <summary>
        /// Первое число это id из БД , второе локальное в коллекции
        /// </summary>
        public Tuple<int, int> LocalIdAndId
        { 
            get => _localIdAndId;
            set
            {
               _localIdAndId = value;
            }
        }
        public LocalUser(User user, int localId)
        {
            DisplayName = user.DisplayName;
            Id = user.Id;
            About = user.About;
            Email = user.Email;
            PhotoUri = user.PhotoUri;
            Follwers = user.Follwers;
            Subscriptions = user.Subscriptions;
            Follower_count = user.Follower_count;
            Following_count = user.Following_count;
            Posts_count = user.Posts_count;
            LocalIdAndId = new Tuple<int, int>((int)Id, localId);
        }
    }
}
