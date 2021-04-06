using System;
using System.Collections.Generic;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;
using Cinemaddict.Models;
using System.IO;
using Firebase.Storage;
using Xamarin.Essentials;
using Newtonsoft.Json.Linq;
using Cinemaddict.Services;

namespace XamarinFirebase.Helper
{

    public class FirebaseHelper
    {
        FirebaseOptions config = new FirebaseOptions();
        FirebaseStorage firebaseStorage;
        FirebaseClient firebase;
        string token = Preferences.Get("token", "");

        public FirebaseHelper()
        {
            config.AsAccessToken = true;
            firebase = new FirebaseClient("https://database-cinemaddict-default-rtdb.europe-west1.firebasedatabase.app/",
                new FirebaseOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult("vue1DZlnwKuoRQpUo7agavzn44StDmZpB2yURKJK")
                });
            firebaseStorage = new FirebaseStorage("database-cinemaddict.appspot.com");
            //,
            //    new FirebaseStorageOptions
            //    {
            //        AuthTokenAsyncFactory = () => Task.FromResult("vue1DZlnwKuoRQpUo7agavzn44StDmZpB2yURKJK")
            //    });
        }

        public async Task<int> GetUserCount()
        {
            //await firebase
            //     .Child("UsersCount")
            //     .PostAsync(0);
            return (await firebase
                    .Child("UsersCount")
                    .OnceAsync<int>()).Select(x => x.Object).FirstOrDefault();
        }

        public async Task UpdateUserCount(bool isReset = false)
        {
            var count = await GetUserCount();

            var updateCount = (await firebase
              .Child("UsersCount")
              .OnceAsync<int>()).Select(x => x.Key).FirstOrDefault();

            if (isReset)
            {
                await firebase
                 .Child("UsersCount")
                 .Child(updateCount)
                 .PutAsync(0); //Ставить 1 
            }
            else
            {
                await firebase
                 .Child("UsersCount")
                 .Child(updateCount)
                 .PutAsync(count + 1);
            }
        }

        public async Task<string> GetFile(string fileName)
        {
            return await firebaseStorage
                .Child("users")
                .Child(token)
                .Child(fileName)
                .GetDownloadUrlAsync();
        }

        /// <returns>Список url фото из Storage</returns>
        /// <summary>
        /// Загружает массив или одно фото в Storage
        /// </summary>
        /// <param name="imageName">Название фото</param>
        /// <param name="userOrPost">Куда ложить фото</param>
        /// <param name="pPost"></param>Если фото для профиля , то по умолчанию = null
        /// <param name="imageStreams">Поток для фото из телефона</param>
        /// <returns>Список url фото из Storage</returns>
        public async Task<List<string>> StoreImages(string imageName, UserOrPost userOrPost, Post pPost = null, User pUser = null, params Stream[] imageStreams)
        {
            if(pUser == null)
            {
                pUser = Util.GetDataLocal();
            }
            
            if (token != null)
            {
                List<string> returnURIs = new List<string>();
                foreach(var imageStream in imageStreams)
                {
                    if(userOrPost.Equals(UserOrPost.User))
                    {
                        var stroageImage = await new FirebaseStorage(
                            "database-cinemaddict.appspot.com")
                            .Child("users")
                            .Child(pUser.Id.ToString())
                            .Child(imageName)
                            .PutAsync(imageStream);
                        returnURIs.Add(stroageImage);
                    }
                    else
                    {
                        if (pPost == null)
                        {
                            return null;
                        }
                        else
                        {
                            var stroageImage = await new FirebaseStorage(
                                "database-cinemaddict.appspot.com")
                                .Child("users")
                                .Child(pUser.Id.ToString())
                                .Child("posts")
                                .Child(pPost.Id.ToString())
                                .Child(imageName)
                                .PutAsync(imageStream);
                            returnURIs.Add(stroageImage);
                        }
                    }
                }
                return returnURIs;
            }
            return null;
        }

        //public async Task<string> GetImage(string imageName)
        //{
        //    var stroageImage = await firebaseStorage
        //        .Child("users")
        //        .Child(token)
        //        .Child(imageName)
        //        .PutAsync(imageStream);
        //    string imgurl = stroageImage;
        //    return imgurl;
        //}


        #region Posts

        public async Task<List<LocalPost>> GetAllNewsPosts()
        {
            var subscriptionsIds = (await GetCurrentUser()).Subscriptions;
            List<LocalPost> items = new List<LocalPost>();
            if (subscriptionsIds != null)
            {
                foreach (var id in subscriptionsIds)
                {
                    User user = await GetUser(id);
                    items.AddRange(
                        (await firebase
                          .Child("users")
                          .Child(id.ToString())
                          .Child("posts")
                          .OnceAsync<Post>()).Select(item => new LocalPost(user, item)).ToList());
                }
            }
            return items;
        }

        public async Task<List<Post>> GetAllPosts()
        {
            return (await firebase
              .Child("users")
              .Child(Preferences.Get("Id", -1).ToString())
              .Child("posts")
              .OnceAsync<Post>()).Select(item => new Post(item)).ToList();
        }

        public async Task<List<Post>> GetAllPosts(int id)
        {
            return (await firebase
              .Child("users")
              .Child(id.ToString())
              .Child("posts")
              .OnceAsync<Post>()).Select(item => new Post(item)).ToList();
        }

        public async Task AddPost(Post item)
        {
            await firebase
              .Child("users")
              .Child(Preferences.Get("Id", -1).ToString())
              .Child("posts")
              .PostAsync(item);
            Preferences.Set("Posts_count", Preferences.Get("Posts_count", 0) + 1);
            await UpdateUser(new User() { Posts_count = Preferences.Get("Posts_count", 0)});
        }

        public async Task<Post> GetPost(int id)
        {
            var allPersons = await GetAllPosts();
            await firebase
              .Child("users")
              .Child(Preferences.Get("Id", -1).ToString())
              .Child("posts")
              .OnceAsync<Post>();
            return allPersons.Where(a => a.Id == id).FirstOrDefault();
        }

        public async Task UpdatePost(Post update)
        {
            int id = Preferences.Get("Id", -1);
            if (update is LocalPost)
            {
                id = (update as LocalPost).AuthorId;
            }
            var toUpdateItem = (await firebase
              .Child("users")
              .Child(id.ToString())
              .Child("posts")
              .OnceAsync<Post>()).Where(a => a.Object.Id == update.Id).FirstOrDefault();

            var postForUpdate = new Post(toUpdateItem);
            postForUpdate.CopyAndReplace(update);

            await firebase
              .Child("users")
              .Child(id.ToString())
              .Child("posts")
              .Child(toUpdateItem.Key)
              .PutAsync(new Post(postForUpdate));
        }

        public async Task DeleteAllPosts()
        {
            for (int i = 0; i < Preferences.Get("Posts_count", 0); i++)
            {
                var toDeletePost = (await firebase
                    .Child("users")
                    .Child(Preferences.Get("Id", -1).ToString())
                    .Child("posts")
                    .OnceAsync<Post>()).Where(a => a.Object.Id == i).FirstOrDefault();
                if(toDeletePost != null)
                {
                    await firebase
                          .Child("users")
                          .Child(Preferences.Get("Id", -1).ToString())
                          .Child("posts")
                          .Child(toDeletePost.Key).DeleteAsync();
                }
            }
        }

        public async Task DeletePost(int id)
        {
            var toDeletePost = (await firebase
              .Child("users")
              .Child(Preferences.Get("Id", -1).ToString())
              .Child("posts")
              .OnceAsync<Post>()).Where(a => a.Object.Id == id).FirstOrDefault();
            await firebase
              .Child("users")
              .Child(Preferences.Get("Id", -1).ToString())
              .Child("posts")
              .Child(toDeletePost.Key).DeleteAsync();
            Preferences.Set("Posts_count", Preferences.Get("Posts_count", 0) - 1);
            await UpdateUser(new User() { Posts_count = Preferences.Get("Posts_count", 0)});
        }
        #endregion

        #region Users
        public async Task<List<User>> GetAllUsers()
        {
            List<User> users = new List<User>();
            //Делаем глубокий вдох ииии... Ахуеваем
            try
            {
                users = (await firebase
                      .Child("users")
                      .OnceSingleAsync<List<JObject>>())
                      .Select(x =>
                           x.Properties().ToList()[0].Value.ToObject<User>())
                      .ToList();
            }
            catch (Exception ex)
            {

            }
            return users;
        }

        public async Task AddUser(User item)
        {
            await firebase
              .Child("users")
              .Child(item.Id.ToString())
              .PostAsync(item);
            await firebase
                .Child("usersId")
                .Child(token)
                .PostAsync(item.Id);
        }

        public async Task<User> GetCurrentUser()
        {
           int id = (await firebase
                .Child("usersId")
                .Child(token)
                .OnceAsync<int>()).Select(x => (int)x.Object).FirstOrDefault();
            return (await firebase
              .Child("users")
              .Child(id.ToString())
              .OnceAsync<User>()).Select(item => new User(item)).ToList().FirstOrDefault();
        }

        public async Task<User> GetUser(int id)
        {
            return (await firebase
              .Child("users")
              .Child(id.ToString())
              .OnceAsync<User>()).Select(item => new User(item)).ToList().FirstOrDefault();
        }

        public async Task UpdateUser(User user, int id)// Нельзя менять другого пользователя, но прийдется.
        {
            var toUpdatePerson = (await firebase
              .Child("users")
              .Child(id.ToString())
              .OnceAsync<User>()).FirstOrDefault();
            var updatePerson = new User(toUpdatePerson);
           
            updatePerson.CopyAndReplace(user);
            await firebase
                .Child("users")
                .Child(id.ToString().ToString())
                .Child(toUpdatePerson.Key)
                .PutAsync(updatePerson);
        }

        public async Task UpdateUser(User user, bool isReset = false)
        {
            var toUpdatePerson = (await firebase
              .Child("users")
              .Child(Preferences.Get("Id", -1).ToString())
              .OnceAsync<User>()).FirstOrDefault();
            var updatePerson = new User(toUpdatePerson);
            if(isReset)
            {
                var swipeUser = new User()
                {
                    About = "Don't delete this user",
                    DisplayName = "General User",
                    Email = "iyongroznyy@gmail.com",
                    Follower_count = 0,
                    Following_count = 1,
                    Id = 0,
                    PhotoUri = "https://firebasestorage.googleapis.com/v0/b/database-cinemaddict.appspot.com/o/users%2FtIIUIM4pk7VxvuLV4KEf1Q89ZXk1?alt=media&token=1bd20704-0609-45c8-887e-89c2dfa9e883",
                    Posts_count = Preferences.Get("Posts_count", 0),
                    Subscriptions = new List<int>() { 0 },
                    Follwers = new List<int>()
                };
                await firebase
                      .Child("users")
                      .Child(Preferences.Get("Id", -1).ToString())
                      .Child(toUpdatePerson.Key)
                      .PutAsync(swipeUser);
                Util.SaveDataLocal(swipeUser);
            }
            else
            {
                updatePerson.CopyAndReplace(user);
                await firebase
                  .Child("users")
                  .Child(Preferences.Get("Id", -1).ToString())
                  .Child(toUpdatePerson.Key)
                  .PutAsync(updatePerson);
                Util.SaveDataLocal(updatePerson);
            }
            
        }

        public async Task DeleteUser(int id)
        {
            var toDeleteUser = (await firebase
              .Child("users")
              .Child(id.ToString())
              .OnceAsync<User>()).FirstOrDefault();
            await firebase.Child("users").Child(id.ToString()).Child(toDeleteUser.Key).DeleteAsync();
        }
        #endregion

        #region Command
        public async Task DeleteAllUser()
        {
            
            int lastID = await GetUserCount();
            for (int i = 1; i < lastID; i++)
            {
                await firebase.Child("users").Child(i.ToString()).DeleteAsync();
            }
            await UpdateUserCount(true);
            await DeleteAllPosts();
            await UpdateUser(null, true);
            
        }
        #endregion
    }
    public interface IFirebaseAuthentication
    {
        Task<string> LoginWithEmailAndPassword(string email, string password);
        Task<string> SignUpWithEmailAndPassword(string email, string password);
        bool SignOut();
        bool IsSignIn();
    }
}