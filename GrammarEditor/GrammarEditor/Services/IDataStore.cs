using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrammarEditor.Services
{
    public interface IDataStore<T>
    {
        
        //TODO: Implement save entire object//  Task<bool> ExportToJSON();
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetGrammarItemsAsync(bool forceRefresh = false);
    }
}
