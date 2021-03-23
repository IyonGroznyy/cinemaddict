using Firebase.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using XamarinFirebase.Helper;

namespace Cinemaddict.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string TitleText { get; set; }
        public string Description { get; set; }
        public string Uri { get; set; }     // Сделать заглушку, когда нет изображения в базе
        public string RepostDecription { get; set; }       // Future feature
        public Tuple<int,List<int>> LikesAndOwners { get; set; }        // item1 - кличевство лайков, item2 - список ID пользователей, поставивших лайк

        public Post()
        {

        }

        public Post(Post item)
        {
            List<object> firstObjs = item.GetType()
                                        .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                        .Select(x => x.GetValue(item))
                                        .ToList();
            int i = 0;
            foreach (var current in GetType().GetProperties())
            {
                current.SetValue(this, firstObjs[i]);
                i++;
            }
        }

        public Post(FirebaseObject<Post> item)
        {
            Description = item.Object.Description;
            TitleText = item.Object.TitleText;
            Id = item.Object.Id;
            Uri = item.Object.Uri;
            RepostDecription = item.Object.RepostDecription;
            LikesAndOwners = item.Object.LikesAndOwners;
        }

        #region FirebaseFunc
        public static async Task<List<LocalPost>> GetAllNewsPosts()
        {
            return await new FirebaseHelper().GetAllNewsPosts();
        }

        public static async Task<string> StoreImages(Stream pStream, string pPath)
        {
           return await new FirebaseHelper().StoreImages(pStream, Path.GetFileName(pPath));
        }

        public static async Task DeletePost(int pId)
        {
            await new FirebaseHelper().DeletePost(pId);
        }

        public static async Task UpdatePost(Post pUpdatePost)
        {
            await new FirebaseHelper().UpdatePost(pUpdatePost);
        }

        public static async Task<List<Post>> GetAllPosts(int pId)
        {
            return await new FirebaseHelper().GetAllPosts(pId);
        }


        #endregion

        public void CopyAndReplace(Post item) // Тут должны быть заполнены только те поля , что надо изменить в this экземпляре
        {
            if(item.Description!=null)
                Description = item.Description;
            if (item.TitleText != null)
                TitleText = item.TitleText;
            if (item.Uri != null)
                Uri = item.Uri;
            if (item.RepostDecription != null)
                RepostDecription = item.RepostDecription;
            if (item.LikesAndOwners != null)
                LikesAndOwners = item.LikesAndOwners;
        }
    }
}