using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Contexts;
using API.Storages.Car;
using API.Storages.Car.Model;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/cars")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarRepository db;

        public CarsController(ApplicationContext context)
        {
            db = new CarRepository(context);

            //if (!context.Cars.Any())
            //{
            //    var manuf = new Manufacturer
            //    {
            //        Id = 1,
            //        Name = "Toyota",
            //        Cars = new List<string> { "Corolla" }
            //    };
            //    var car = new Car 
            //    {
            //        Id = 1, 
            //        ModelName = "Corolla", 
            //        ManufacturerId = 1, 
            //        EngineId = 1
            //    };
            //    var engine = new Engine
            //    {
            //        Id = 1,
            //        Type = "fuel", 
            //        Cars = new List<string> { "Corolla" }
            //    };
            //    context.Manufacturers.Add(manuf);
            //    context.Engines.Add(engine);
            //    context.Cars.Add(car);
            //    context.SaveChanges();
            //}
        }

        //public CarsController(ModelsContext context)
        //{
        //    db = context;
        //if (!db.Cars.Any())
        //{
        //    var manuf = new Manufacturer { Name = "Toyota" };
        //    var car = new Car { ModelName = "Biba" };
        //    var engine = new Engine { Type = "fuel" };

        //    db.Cars.Add(car);

        //    manuf.Cars = new List<string> { car.ModelName };
        //    engine.Cars = new List<string> { car.ModelName };

        //    db.Manufacturers.Add(manuf);
        //    db.Engines.Add(engine);

        //    db.SaveChanges();
        //}
        //else
        //{
        //    var manuf = db.Manufacturers.Find(1);
        //    var engine = db.Engines.Find(1);
        //    var car = db.Cars.Find(1);
        //    manuf.Cars = new List<string> { car.ModelName };
        //    engine.Cars = new List<string> { car.ModelName };
        //}
        //}

        [HttpGet]
        public async Task<List<Car>> GetCars()
        {
            return await db.GetCarsListAsync();
            //return await db.Cars
            //    .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await db.GetCarAsync(id);
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
            //var manuf = db.Manufacturers.Find(car.ManufacturerId);
            //var engine = db.Engines.Find(car.EngineId);
            //if (manuf == null || engine == null)
            try
            {
                await db.CreateAsync(car);
            }
            catch (NullReferenceException e)
            {
                return BadRequest(e.Message);
            }

            //manuf.Cars.Add(car.ModelName);
            //engine.Cars.Add(car.ModelName);
            //db.Cars.Add(car);
            await db.SaveAsync();
            return Ok(car);
        }
    }
}
