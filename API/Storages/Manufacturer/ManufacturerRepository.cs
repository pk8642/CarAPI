using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace API.Storages.Manufacturer
{
    /// <summary>
    /// Класс репозитория модели класса Manufacturer, реализующий методы интерфейса IRepository.
    /// </summary>
    public class ManufacturerRepository : IRepository<Model.Manufacturer>
    {
        private readonly ApplicationContext db;
        private bool _disposed;

        /// <summary>
        /// Конструктор данного класса.
        /// Использует переданный контекст данных для совершения операций с БД.
        /// </summary>
        /// <param name="context"> Контекст данных </param>
        public ManufacturerRepository(ApplicationContext context)
        {
            db = context;
        }

        /// <summary>
        /// Метод, возвращающий объект производителя по его id
        /// </summary>
        /// <param name="id"> Уникальный номер производителя </param>
        public async Task<Model.Manufacturer> GetManufacturerAsync(int id)
        {
            return await db.Manufacturers.FindAsync(id);
        }

        /// <summary>
        /// Метод, возвращающий список всех производителей
        /// </summary>
        public async Task<List<Model.Manufacturer>> GetManufacturersListAsync()
        {
            return await db.Manufacturers.ToListAsync();
        }

        /// <summary>
        /// Метод, добаляющий нового производителя в БД
        /// </summary>
        /// <param name="item"> Объект производителя </param>
        public async Task<EntityEntry<Model.Manufacturer>> CreateAsync(Model.Manufacturer item)
        {
            return await db.Manufacturers.AddAsync(item);
        }

        /// <summary>
        /// Метод выполняющий обновление БД
        /// </summary>
        /// <param name="item"> Объект производителя </param>
        public void Update(Model.Manufacturer item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        /// <summary>
        /// Метод, сохраняющий изменения в БД
        /// </summary>
        public async Task<int> SaveAsync()
        {
            return await db.SaveChangesAsync();
        }

        /// <summary>
        /// Освобождает неиспользующиеся ресурсы
        /// </summary>
        /// <param name="disposing"></param>
        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            _disposed = true;
        }

        /// <summary>
        /// Метод, освобождающий неиспользующиеся ресурсы
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}