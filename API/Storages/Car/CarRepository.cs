using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace API.Storages.Car
{
    /// <summary>
    /// Класс репозитория модели класса Car, реализующий методы интерфейса IRepository. 
    /// </summary>
    public class CarRepository : IRepository<Model.Car>
    {
        private readonly ApplicationContext _db;
        private bool _disposed;

        /// <summary>
        /// Конструктор данного класса.
        /// Использует переданный контекст данных для совершения операций с БД.
        /// </summary>
        /// <param name="context"> Контекст данных </param>
        public CarRepository(ApplicationContext context)
        {
            _db = context;
        }

        /// <summary>
        /// Метод, возвращающий объект машины по ее id
        /// </summary>
        /// <param name="id"> Уникальный номер машины </param>
        public async Task<Model.Car> GetCarAsync(int id)
        {
            return await _db.Cars.FindAsync(id);
        }

        /// <summary>
        /// Метод, возвращающий список всех машин
        /// </summary>
        public async Task<List<Model.Car>> GetCarsListAsync()
        {
            return await _db.Cars.ToListAsync();
        }

        /// <summary>
        /// Метод, добаляющий новую машину в БД
        /// </summary>
        /// <param name="item"> Объект машины </param>
        public async Task<EntityEntry<Model.Car>> CreateAsync(Model.Car item)
        {
            var manuf = await _db.Manufacturers.FindAsync(item.ManufacturerId);
            var engine = await _db.Engines.FindAsync(item.EngineId);
            if (manuf == null || engine == null)
                throw new NullReferenceException();
            manuf.Cars.Add(item.ModelName);
            engine.Cars.Add(item.ModelName);
            return await _db.Cars.AddAsync(item);
        }

        /// <summary>
        /// Метод выполняющий обновление БД
        /// </summary>
        /// <param name="item"> Объект машины </param>
        public void Update(Model.Car item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        /// <summary>
        /// Метод, сохраняющий изменения в БД
        /// </summary>
        public async Task<int> SaveAsync()
        {
            return await _db.SaveChangesAsync();
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
                    _db.Dispose();
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
