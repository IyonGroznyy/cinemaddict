﻿using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Cinemaddict.Models;

namespace XamarinFirebase.Helper
{

    public class FirebaseHelper
    {
        FirebaseClient firebase = new FirebaseClient("https://database-cinemaddict-default-rtdb.europe-west1.firebasedatabase.app/");

        public async Task<List<Item>> GetAllPersons()
        {

            return (await firebase
              .Child("Items")
              .OnceAsync<Item>()).Select(item => new Item
              {
                  Text = item.Object.Text,
                  Id = item.Object.Id
              }).ToList();
        }

        public async Task AddPerson(Item item)
        {
            item.Id = 0;
            await firebase
              .Child("Items")
              .PostAsync(item);
        }

        public async Task<Item> GetPerson(int id)
        {
            var allPersons = await GetAllPersons();
            await firebase
              .Child("Items")
              .OnceAsync<Item>();
            return allPersons.Where(a => a.Id == id).FirstOrDefault();
        }

        public async Task UpdatePerson(int id, string text)
        {
            var toUpdatePerson = (await firebase
              .Child("Items")
              .OnceAsync<Item>()).Where(a => a.Object.Id == id).FirstOrDefault();

            await firebase
              .Child("Items")
              .Child(toUpdatePerson.Key)
              .PutAsync(new Item() { Id = id, Text = text });
        }

        public async Task DeletePerson(int id)
        {
            var toDeletePerson = (await firebase
              .Child("Items")
              .OnceAsync<Item>()).Where(a => a.Object.Id == id).FirstOrDefault();
            await firebase.Child("Items").Child(toDeletePerson.Key).DeleteAsync();

        }
    }
    public interface IFirebaseAuthentication
    {
        Task<string> LoginWithEmailAndPassword(string email, string password);
        bool SignOut();
        bool IsSignIn();
    }
}