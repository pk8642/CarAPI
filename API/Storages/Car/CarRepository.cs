using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace API.Storages.Car
{
    public class CarRepository : IRepository<Model.Car>
    {
        private readonly ApplicationContext _db;
        private bool _disposed;

        //public CarRepository()
        //{
        //    db = new ModelsContext();
        //}

        public CarRepository(ApplicationContext context)
        {
            _db = context;
        }

        public async Task<Model.Car> GetCarAsync(int id)
        {
            return await _db.Cars.FindAsync(id);
        }

        public async Task<List<Model.Car>> GetCarsListAsync()
        {
            return await _db.Cars.ToListAsync();
        }

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

        public void Update(Model.Car item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public async void DeleteAsync(int id)
        {
            var car = await _db.Cars.FindAsync(id);
            if (car != null)
                _db.Cars.Remove(car);
        }

        public async Task<int> SaveAsync()
        {
            return await _db.SaveChangesAsync();
        }


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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
