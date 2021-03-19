using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cinemaddict.Models
{
    public class LocalPost : Post
    {
        public string AuthorDisplayName { get; set; }
        public string AuthorPhotoUri { get; set; }
        public int AuthorId { get; set; }

        /// <summary>
        /// Левый это идентификатор автора, правый - это поле из базового класа Post - LikesAndOwners (количество и ID поставивших лайк)
        /// </summary>
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

        public LocalPost(Post item)
        {
            Id = item.Id;
            TitleText = item.TitleText;
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

        public LocalPost(User author, FirebaseObject<Post> item) : base(item)
        {
            AuthorDisplayName = author.DisplayName;
            AuthorPhotoUri = author.PhotoUri;
            AuthorId = (int)author.Id;
        }
    }
}
