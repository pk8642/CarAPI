using API.Contexts;
using API.Storages.Car;
using API.Storages.Car.Model;
using API.Storages.Engine.Model;
using API.Storages.Manufacturer.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/cars")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarRepository _db;

        public CarsController(ApplicationContext context)
        {
            _db = new CarRepository(context);

            if (!context.Cars.Any())
            {
                var manuf = new Manufacturer
                {
                    Id = 1, Name = "Toyota", Cars = new List<string> { "Corolla" }
                };
                var car = new Car
                {
                    Id = 1, ModelName = "Corolla", ManufacturerId = 1, EngineId = 1
                };
                var engine = new Engine
                {
                    Id = 1, Type = "Fuel", Cars = new List<string> { "Corolla" }
                };
                context.Manufacturers.Add(manuf);
                context.Engines.Add(engine);
                context.Cars.Add(car);
                context.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<List<Car>> GetCars()
        {
            return await _db.GetCarsListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _db.GetCarAsync(id);
            if (car == null)
                return NotFound();
            return car;
        }

        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            if (car == null)
            {
                return BadRequest();
            }

            try
            {
                await _db.CreateAsync(car);
            }
            catch (NullReferenceException e)
            {
                return BadRequest(e.Message);
            }
            
            await _db.SaveAsync();
            return Ok(car);
        }
    }
}
