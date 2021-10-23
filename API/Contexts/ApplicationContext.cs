using API.Storages.Car.Model;
using API.Storages.Engine.Model;
using API.Storages.Manufacturer.Model;
using Microsoft.EntityFrameworkCore;

namespace API.Contexts
{
    public sealed class ApplicationContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Engine> Engines { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}