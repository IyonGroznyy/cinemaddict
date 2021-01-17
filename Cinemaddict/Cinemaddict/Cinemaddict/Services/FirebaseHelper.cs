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

namespace XamarinFirebase.Helper
{

    public class FirebaseHelper
    {
        FirebaseOptions config = new FirebaseOptions();

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
        }
        public async Task<List<Item>> GetAllPersons()
        {
            return (await firebase
              .Child("users")
              .Child(token)
              .OnceAsync<Item>()).Select(item => new Item
              {
                  Text = item.Object.Text,
                  Id = item.Object.Id
              }).ToList();
        }

        public async Task AddPerson(Item item)
        {
            //item.uid = "RrhVzHwNUZSaZwafdKyyAhkpwZy1";
            await firebase
              .Child("users")
              .Child(token)
              .PostAsync(item);
        }

        public async Task<Item> GetPerson(int id)
        {
            var allPersons = await GetAllPersons();
            await firebase
              .Child("users")
              .Child(token)
              .OnceAsync<Item>();
            return allPersons.Where(a => a.Id == id).FirstOrDefault();
        }

        public async Task UpdatePerson(int id, string text)
        {
            var toUpdatePerson = (await firebase
              .Child("users")
              .Child(token)
              .OnceAsync<Item>()).Where(a => a.Object.Id == id).FirstOrDefault();

            await firebase
              .Child("users")
              .Child(token)
              .Child(toUpdatePerson.Key)
              .PutAsync(new Item() { Id = id, Text = text });
        }

        public async Task DeletePerson(int id)
        {
            var toDeletePerson = (await firebase
              .Child("users")
              .Child(token)
              .OnceAsync<Item>()).Where(a => a.Object.Id == id).FirstOrDefault();
            await firebase.Child(token).Child(toDeletePerson.Key).DeleteAsync();

        }
    }
    public interface IFirebaseAuthentication
    {
        Task<string> LoginWithEmailAndPassword(string email, string password);
        Task<string> SignUpWithEmailAndPassword(string email, string password);
        bool SignOut();
        bool IsSignIn(ref string token);
    }
}