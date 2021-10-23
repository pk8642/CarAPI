using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace API.Storages.Engine
{
    public class EngineRepository : IRepository<Model.Engine>
    {
        private readonly ApplicationContext _db;
        private bool _disposed;

        //public EngineRepository()
        //{
        //    db = new ModelsContext();
        //}

        public EngineRepository(ApplicationContext context)
        {
            _db = context;
        }

        public async Task<Model.Engine> GetEngineAsync(int id)
        {
            return await _db.Engines.FindAsync(id);
        }

        public async Task<List<Model.Engine>> GetEnginesListAsync()
        {
            return await _db.Engines.ToListAsync();
        }

        public async Task<EntityEntry<Model.Engine>> CreateAsync(Model.Engine item)
        {
            return await _db.Engines.AddAsync(item);
        }

        public void Update(Model.Engine item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public async void DeleteAsync(int id)
        {
            var engine = await _db.Engines.FindAsync(id);
            if (engine != null)
                _db.Engines.Remove(engine);
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