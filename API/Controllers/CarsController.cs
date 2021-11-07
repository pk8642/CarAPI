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
    /// <summary>
    /// Класс контроллера для сущности машин
    /// </summary>
    [Route("api/cars")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarRepository _db;

        /// <summary>
        /// Конструктор контроллера. При отсутствии данных в БД, добавляет строку по умолчанию
        /// </summary>
        /// <param name="context"> Контекст приложения </param>
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

        /// <summary>
        /// Метод, возвращающий список всех машин из БД
        /// </summary>
        [HttpGet]
        public async Task<List<Car>> GetCarsAsync()
        {
            return await _db.GetCarsListAsync();
        }

        /// <summary>
        /// Метод, возвращающий экземпляр машины из БД по id
        /// </summary>
        /// <param name="id"> Некоторое число, уникальный номер машины в БД </param>
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCarAsync(int id)
        {
            var car = await _db.GetCarAsync(id);
            if (car == null)
                return NotFound();
            return car;
        }

        /// <summary>
        /// Метод, Добавляющий новую машину в БД
        /// </summary>
        /// <param name="car"> Объект машины </param>
        [HttpPost]
        public async Task<ActionResult<Car>> PostCarAsync(Car car)
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
