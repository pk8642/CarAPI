using System.Collections.Generic;
using System.Threading.Tasks;
using API.Contexts;
using API.Storages.Car.Model;
using API.Storages.Manufacturer;
using API.Storages.Manufacturer.Model;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/manufacturers")]
    [ApiController]
    public class ManufacturersController : ControllerBase
    {
        private readonly ManufacturerRepository db;

        public ManufacturersController(ApplicationContext context)
        {
            db = new ManufacturerRepository(context);

            //if (context.Manufacturers.Any())
            //    return;
            //db.Create(new Manufacturer { Id = 1, Name = "Toyota", Cars = new List<string> { "Corolla" } });
            //db.Save();
        }

        [HttpGet]
        public async Task<IEnumerable<Manufacturer>> GetManufacturers()
        {
            return await db.GetManufacturersListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Manufacturer>> GetManufacturer(int id)
        {
            var manufacturer = await db.GetManufacturerAsync(id);
            if (manufacturer == null)
                return NotFound();
            return manufacturer;
        }

        [HttpPost]
        public async Task<ActionResult<Car>> PostManufacturer(Manufacturer manufacturer)
        {
            if (manufacturer == null)
            {
                return BadRequest();
            }

            manufacturer.Cars = new List<string>();
            await db.CreateAsync(manufacturer);
            await db.SaveAsync();
            return Ok(manufacturer);
        }
    }
}