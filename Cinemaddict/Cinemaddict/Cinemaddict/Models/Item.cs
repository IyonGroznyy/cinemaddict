using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Cinemaddict.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public string Uri { get; set; }
        public string RepostDecription { get; set; }
        public Tuple<int,List<int>> LikesAndOwners { get; set; }

        public Item()
        {

        }

        public Item(Item item)
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

        public Item(FirebaseObject<Item> item)
        {
            Description = item.Object.Description;
            Text = item.Object.Text;
            Id = item.Object.Id;
            Uri = item.Object.Uri;
            RepostDecription = item.Object.RepostDecription;
            LikesAndOwners = item.Object.LikesAndOwners;
        }

        public void CopyAndReplace(Item item) // Тут должны быть заполнены только те поля , что надо изменить в this экземпляре
        {
            if(item.Description!=null)
                Description = item.Description;
            if (item.Text != null)
                Text = item.Text;
            if (item.Uri != null)
                Uri = item.Uri;
            if (item.RepostDecription != null)
                RepostDecription = item.RepostDecription;
            if (item.LikesAndOwners != null)
                LikesAndOwners = item.LikesAndOwners;
        }
    }
}