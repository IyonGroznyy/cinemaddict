using Cinemaddict.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinemaddict.Services
{
    public class MockDataStore : IDataStore<Post>
    {
        readonly List<Post> items;

        public MockDataStore()
        {
            items = new List<Post>()
            {
            };
        }

        public async Task<bool> AddItemAsync(Post item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Post item)
        {
            var oldItem = items.Where((Post arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Post arg) => arg.Id.ToString() == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Post> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id.ToString() == id));
        }

        public async Task<IEnumerable<Post>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}