using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace API.Storages.Engine
{
    /// <summary>
    /// Класс репозитория модели класса Engine, реализующий методы интерфейса IRepository. 
    /// </summary>
    public class EngineRepository : IRepository<Model.Engine>
    {
        private readonly ApplicationContext _db;
        private bool _disposed;

        /// <summary>
        /// Конструктор данного класса.
        /// Использует переданный контекст данных для совершения операций с БД.
        /// </summary>
        /// <param name="context"> Контекст данных </param>
        public EngineRepository(ApplicationContext context)
        {
            _db = context;
        }

        /// <summary>
        /// Метод, возвращающий объект двигателя по его id
        /// </summary>
        /// <param name="id"> Уникальный номер двигателя </param>
        public async Task<Model.Engine> GetEngineAsync(int id)
        {
            return await _db.Engines.FindAsync(id);
        }

        /// <summary>
        /// Метод, возвращающий список всех двигателей
        /// </summary>
        public async Task<List<Model.Engine>> GetEnginesListAsync()
        {
            return await _db.Engines.ToListAsync();
        }

        /// <summary>
        /// Метод, добаляющий новый двигатель в БД
        /// </summary>
        /// <param name="item"> Объект двигателя </param>
        public async Task<EntityEntry<Model.Engine>> CreateAsync(Model.Engine item)
        {
            return await _db.Engines.AddAsync(item);
        }

        /// <summary>
        /// Метод выполняющий обновление БД
        /// </summary>
        /// <param name="item"> Объект двигателя </param>
        public void Update(Model.Engine item)
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