using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Cinemaddict.Models;
using Xamarin.Forms;
using System.IO;
using Firebase.Storage;

namespace XamarinFirebase.Helper
{

    public class FirebaseHelper
    {
        FirebaseOptions config = new FirebaseOptions();
        FirebaseStorage firebaseStorage;
        FirebaseClient firebase;
        string token = Application.Current.Properties["token"] as string;

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

        public async Task<string> GetFile(string fileName)
        {
            return await firebaseStorage
                .Child("users")
                .Child(token)
                .Child(fileName)
                .GetDownloadUrlAsync();
        }

        public async Task<string> StoreImages(Stream imageStream, string imageName)
        {
            var stroageImage = await new FirebaseStorage(
                "database-cinemaddict.appspot.com")
                .Child("users")
                .Child(token)
                .PutAsync(imageStream);
            return stroageImage;
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
        public async Task<List<Item>> GetAllPosts()
        {
            return (await firebase
              .Child("posts")
              .Child(token)
              .OnceAsync<Item>()).Select(item => new Item
              {
                  Description = item.Object.Description,
                  Text = item.Object.Text,
                  Id = item.Object.Id
              }).ToList();
        }

        public async Task AddPost(Item item)
        {
            //item.uid = "RrhVzHwNUZSaZwafdKyyAhkpwZy1";
            await firebase
              .Child("posts")
              .Child(token)
              .PostAsync(item);
        }

        public async Task<Item> GetPost(int id)
        {
            var allPersons = await GetAllPosts();
            await firebase
              .Child("posts")
              .Child(token)
              .OnceAsync<Item>();
            return allPersons.Where(a => a.Id == id).FirstOrDefault();
        }

        public async Task UpdatePost(int id, string text, string description)
        {
            var toUpdatePerson = (await firebase
              .Child("posts")
              .Child(token)
              .OnceAsync<Item>()).Where(a => a.Object.Id == id).FirstOrDefault();

            await firebase
              .Child("posts")
              .Child(token)
              .Child(toUpdatePerson.Key)
              .PutAsync(new Item() { Id = id, Text = text , Description = description });
        }

        public async Task DeletePost(int id)
        {
            var toDeletePerson = (await firebase
              .Child("posts")
              .Child(token)
              .OnceAsync<Item>()).Where(a => a.Object.Id == id).FirstOrDefault();
            await firebase.Child("posts").Child(token).Child(toDeletePerson.Key).DeleteAsync();
        }
        #endregion

        #region Users
        public async Task<List<User>> GetAllUsers()
        {
            return (await firebase
              .Child("users")
              .Child(token)
              .OnceAsync<User>()).Select(item => new User
              {
                  DisplayName = item.Object.DisplayName,
                  Id = item.Object.Id,
                  About = item.Object.About,
                  Email = item.Object.Email,
                  PhotoUri = item.Object.PhotoUri
              }).ToList();
        }

        public async Task AddUser(User item)
        {
            await firebase
              .Child("users")
              .Child(token)
              .PostAsync(item);
        }

        public async Task<User> GetUser(int id)
        {
            var allPersons = await GetAllUsers();
            await firebase
              .Child("users")
              .Child(token)
              .OnceAsync<User>();
            return allPersons.Where(a => a.Id == id).FirstOrDefault();
        }

        public async Task UpdateUser(int id, string displayName, string about, string email, string photoUri)
        {
            var toUpdatePerson = (await firebase
              .Child("users")
              .Child(token)
              .OnceAsync<User>()).Where(a => a.Object.Id == id).FirstOrDefault();

            await firebase
              .Child("users")
              .Child(token)
              .Child(toUpdatePerson.Key)
              .PutAsync(new User() { Id = id,  DisplayName = displayName, About = about, Email = email, PhotoUri = photoUri });
        }

        public async Task DeleteUser(int id)
        {
            var toDeleteUser = (await firebase
              .Child("users")
              .Child(token)
              .OnceAsync<User>()).Where(a => a.Object.Id == id).FirstOrDefault();
            await firebase.Child("users").Child(token).Child(toDeleteUser.Key).DeleteAsync();
        }
        #endregion
    }
    public interface IFirebaseAuthentication
    {
        Task<string> LoginWithEmailAndPassword(string email, string password);
        Task<string> SignUpWithEmailAndPassword(string email, string password);
        bool SignOut();
        bool IsSignIn(ref string token);
    }
}