using System;
using System.Threading.Tasks;

namespace Framework.Application
{
    public interface ICacheProvider
    {
        Task<bool> ExistAsync(string key);
        Task AddAsync<T>(string key, T value);
        Task<T> GetAsync<T>(string key) where T : class;
    }
}