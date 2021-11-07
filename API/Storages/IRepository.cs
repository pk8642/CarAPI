using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace API.Storages
{
    /// <summary>
    /// Интерфейс репозитория для возможности тестирования без необходимости подключения к БД
    /// </summary>
    public interface IRepository<T> : IDisposable where T : class
    {
        /// <summary>
        /// Метод, добаляющий некоторую сущность в БД
        /// </summary>
        /// <param name="item"> Объект некоторого класса </param>
        Task<EntityEntry<T>> CreateAsync(T item);

        /// <summary>
        /// Метод, выполняющий обновление БД
        /// </summary>
        /// <param name="item"> Объект некоторого класса </param>
        void Update(T item);

        /// <summary>
        /// Метод, сохраняющий изменения в БД
        /// </summary>
        Task<int> SaveAsync();

    }
}