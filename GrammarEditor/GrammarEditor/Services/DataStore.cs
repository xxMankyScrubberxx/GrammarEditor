using GrammarEditor.Models;
using GrammarEditor.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GrammarEditor.Services
{
    public class DataStore : IDataStore<Item>
    {
        readonly List<Item> items;

        public DataStore()
        {
            string sData = GrammarSettings.GrammarDataLocalStore;
            var array = JArray.Parse(sData);

            foreach (var item in array)
            {
                //new Item { Id = Guid.NewGuid().ToString(), MSG_EN = "First item", MSG_RU="This is an item description." },
                //new Item { Id = Guid.NewGuid().ToString(), MSG_EN = "Second item", MSG_RU="This is an item description." },
                //new Item { Id = Guid.NewGuid().ToString(), MSG_EN = "Third item", MSG_RU="This is an item description." },
                //new Item { Id = Guid.NewGuid().ToString(), MSG_EN = "Fourth item", MSG_RU="This is an item description." },
                //new Item { Id = Guid.NewGuid().ToString(), MSG_EN = "Fifth item", MSG_RU="This is an item description." },
                //new Item { Id = Guid.NewGuid().ToString(), MSG_EN = "Sixth item", MSG_RU="This is an item description." }
            };

        }

        
        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}