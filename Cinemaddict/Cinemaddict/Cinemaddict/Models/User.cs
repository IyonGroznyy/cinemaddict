using Firebase.Database;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using XamarinFirebase.Helper;

namespace Cinemaddict.Models
{
    public class User
    {
        public int? Id { get; set; }
        public int? Posts_count { get; set; }
        public int? Follower_count { get; set; }
        public int? Following_count { get; set; }
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

        public void CopyAndReplace(User user) // Тут должны быть заполнены только те поля , что надо изменить в this экземпляре
        {
            List<object> firstObjs = user.GetType()
                                         .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                         .Select(x => x.GetValue(user))
                                         .ToList();
            int i = 0;
            foreach (var current in GetType().GetProperties())
            {
                
                if (current.GetValue(this) is List<int> && current.GetValue(this) != null)
                {
                    if (firstObjs[i] != null)
                    {
                        List<int> currArr = (List<int>)current.GetValue(this);
                        List<int> sendArr = (List<int>)firstObjs[i];
                        if(currArr.Exists(x => sendArr.Find(y => x.Equals(y)) > 0))
                        {
                            sendArr.ForEach(x => currArr.Remove(x));
                        }
                        else
                        {
                            currArr = currArr.Union(sendArr).ToList();
                            current.SetValue(this, currArr);
                        }
                        if (current.Name.Equals("Subscriptions"))
                        {
                            Following_count = currArr.Count;
                        }
                        if (current.Name.Equals("Follwers"))
                        {
                            Follower_count = currArr.Count;
                        }
                    }
                }
                else  if (firstObjs[i] != null)
                {
                    current.SetValue(this, firstObjs[i]);
                }
                i++;
            }
        }

        #region FirebaseFunc
        public static async Task<User> GetCurrentUser()
        {
           return await new FirebaseHelper().GetCurrentUser();
        }

        public static async Task UpdateUser(User pUser, int pId)
        {
             await new FirebaseHelper().UpdateUser(pUser, pId);
        }

        public static async Task UpdateUser(User pUser)
        {
            await new FirebaseHelper().UpdateUser(pUser);
        }

        public static async Task DeleteAllUser()
        {
            await new FirebaseHelper().DeleteAllUser();
        }

        public static async Task AddUser(User pUser)
        {
            await new FirebaseHelper().AddUser(pUser);
        }

        public static async Task UpdateUserCount()
        {
            await new FirebaseHelper().UpdateUserCount();
        }

        public static async Task<User> GetUser(int pId)
        {
            return await new FirebaseHelper().GetUser(pId);
        }

        public static async Task<int> GetUserCount()
        {
            return await new FirebaseHelper().GetUserCount();
        }

        public static async Task<List<User>> GetAllUsers()
        {
            return await new FirebaseHelper().GetAllUsers();
        }
        #endregion
    }
}
