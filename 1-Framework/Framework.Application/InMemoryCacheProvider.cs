using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Framework.Application
{
    public class InMemoryCacheProvider : ICacheProvider
    {
        //private InMemoryCacheProvider()
        //{
        //    Storage = new Dictionary<string, object>();
        ////}
        //private Dictionary<string, object> Storage { get; set; }
        //public async Task  AddAsync(string key, object value)
        //{
        //    Storage[key] = value;
        //}

        //public async Task<object> GetAsync(string key)
        //{
        //    return Storage.ContainsKey(key)? Storage[key]:null;
        //}

        

        public InMemoryCacheProvider()
        {
        }
        private Dictionary<string, object> Storage = new Dictionary<string, object>();
        public async Task AddAsync<T>(string key, T value)
        {
            Storage[key] = value;
        }
        public async Task<bool> ExistAsync(string key)
        {
            if(null != Storage)
            return Storage.ContainsKey(key) ? true : false;

            return false;
        }
        public async Task<T> GetAsync<T>(string key)
        {
            T result=default(T);
            if (Storage.ContainsKey(key))
                result = (T)Convert.ChangeType(Storage[key], typeof(T)); 
              return result;
        }
    }
}