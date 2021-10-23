using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace API.Storages.Manufacturer
{
    public class ManufacturerRepository : IRepository<Model.Manufacturer>
    {
        private readonly ApplicationContext db;
        private bool _disposed;

        //public ManufacturerRepository()
        //{
        //    db = new ModelsContext();
        //}

        public ManufacturerRepository(ApplicationContext context)
        {
            db = context;
        }

        public async Task<Model.Manufacturer> GetManufacturerAsync(int id)
        {
            return await db.Manufacturers.FindAsync(id);
        }

        public async Task<List<Model.Manufacturer>> GetManufacturersListAsync()
        {
            return await db.Manufacturers.ToListAsync();
        }
        public async Task<EntityEntry<Model.Manufacturer>> CreateAsync(Model.Manufacturer item)
        {
            return await db.Manufacturers.AddAsync(item);
        }

        public void Update(Model.Manufacturer item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public async void DeleteAsync(int id)
        {
            var manuf = await db.Manufacturers.FindAsync(id);
            if (manuf != null)
                db.Manufacturers.Remove(manuf);
        }

        public async Task<int> SaveAsync()
        {
            return await db.SaveChangesAsync();
        }

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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}