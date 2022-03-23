using GrammarEditor.Models;
using GrammarEditor.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GrammarEditor.Services
{
    public class DataStore : IDataStore<GrammarItem>
    {
        //readonly List<GrammarItem> items;
        public ObservableCollection<GrammarItem> items { get; private set; }

        public DataStore()
        {
            //items = new List<GrammarItem>();
            items = new ObservableCollection<GrammarItem>();
            string sData = GrammarSettings.GrammarDataLocalStore;
            var array = JArray.Parse(sData);

            foreach (var item in array)
            {
                items.Add(
                    new GrammarItem 
                    { 
                        MSG_ID = item["MSG_ID"].ToString(), 
                        MSG_EN = item["EN"].ToString(), 
                        MSG_RU = item["RU"].ToString(), 
                        MSG_CAT = item["CATEGORY"].ToString(),
                        BIBLIOGRAPHY = item["BIBLIOGRAPHY"].ToString(),
                        EN_STATUS = item["EN_STATUS"].ToString(),
                        RU_STATUS = item["RU_STATUS"].ToString(),
                        NOTES = item["NOTES"].ToString(),
                        CATEGORIES = item["CATEGORIES"].ToString()
                    }
                );
            };

        }

        
        public async Task<bool> AddItemAsync(GrammarItem item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(GrammarItem item)
        {
            var oldItem = items.Where((GrammarItem arg) => arg.MSG_ID == item.MSG_ID).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((GrammarItem arg) => arg.MSG_ID == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<GrammarItem> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.MSG_ID == id));
        }

        public async Task<IEnumerable<GrammarItem>> GetGrammarItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}