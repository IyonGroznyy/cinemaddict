using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cinemaddict.Models
{
    public class LocalPost : Item
    {
        public string AuthorDisplayName { get; set; }
        public string AuthorPhotoUri { get; set; }
        public int AuthorId { get; set; }
        public Tuple<int, Tuple<int, List<int>>> LikeInfo // Пожалуйста , не убивайте за это, меня заставили это написать
        {
            get
            {
               return new Tuple<int, Tuple<int, List<int>>>(AuthorId, LikesAndOwners);
            }
        }               

        public LocalPost()
        {

        }
        public LocalPost(Item item)
        {
            Id = item.Id;
            Text = item.Text;
            Description = item.Description;
            Uri = item.Uri;
            RepostDecription = item.RepostDecription;
            LikesAndOwners = item.LikesAndOwners;
    }
        public LocalPost(User author)
        {
            AuthorDisplayName = author.DisplayName;
            AuthorPhotoUri = author.PhotoUri;
            AuthorId = (int)author.Id;
        }
        public LocalPost(User author, FirebaseObject<Item> item) : base(item)
        {
            AuthorDisplayName = author.DisplayName;
            AuthorPhotoUri = author.PhotoUri;
            AuthorId = (int)author.Id;
        }
    }
}
