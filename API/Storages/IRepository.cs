using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace API.Storages
{
    public interface IRepository<T> : IDisposable where T : class
    {
        Task<EntityEntry<T>> CreateAsync(T item);
        void Update(T item);
        void DeleteAsync(int id);
        Task<int> SaveAsync();

    }
}