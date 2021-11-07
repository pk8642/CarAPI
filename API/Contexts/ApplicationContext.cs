using API.Storages.Car.Model;
using API.Storages.Engine.Model;
using API.Storages.Manufacturer.Model;
using Microsoft.EntityFrameworkCore;

namespace API.Contexts
{
    /// <summary>
    /// Класс контекста приложения
    /// </summary>
    public sealed class ApplicationContext : DbContext
    {
        /// <summary>
        /// Множество машин
        /// </summary>
        public DbSet<Car> Cars { get; set; }
        
        /// <summary>
        /// Множество двигателей
        /// </summary>
        public DbSet<Engine> Engines { get; set; }
        
        /// <summary>
        /// Множество производителей
        /// </summary>
        public DbSet<Manufacturer> Manufacturers { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="options"></param>
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}