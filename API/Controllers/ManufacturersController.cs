using System.Collections.Generic;
using System.Linq;
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
        private readonly ManufacturerRepository _db;

        public ManufacturersController(ApplicationContext context)
        {
            _db = new ManufacturerRepository(context);

            if (context.Manufacturers.Any())
                return;
            context.Manufacturers.Add(new Manufacturer
            {
                Id = 1, Name = "Toyota", Cars = new List<string> { "Corolla" }
            });
            context.SaveChanges();
        }

        [HttpGet]
        public async Task<IEnumerable<Manufacturer>> GetManufacturers()
        {
            return await _db.GetManufacturersListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Manufacturer>> GetManufacturer(int id)
        {
            var manufacturer = await _db.GetManufacturerAsync(id);
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
            await _db.CreateAsync(manufacturer);
            await _db.SaveAsync();
            return Ok(manufacturer);
        }
    }
}