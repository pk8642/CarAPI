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
    /// <summary>
    /// Класс контроллера для сущности производителей
    /// </summary>
    [Route("api/manufacturers")]
    [ApiController]
    public class ManufacturersController : ControllerBase
    {
        private readonly ManufacturerRepository _db;

        /// <summary>
        /// Конструктор контроллера. При отсутствии данных в БД, добавляет строку по умолчанию
        /// </summary>
        /// <param name="context"> Контекст приложения </param>
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

        /// <summary>
        /// Метод, возвращающий список всех производителей из БД
        /// </summary>
        [HttpGet]
        public async Task<IEnumerable<Manufacturer>> GetManufacturersAsync()
        {
            return await _db.GetManufacturersListAsync();
        }

        /// <summary>
        /// Метод, возвращающий экземпляр производителя из БД по id
        /// </summary>
        /// <param name="id"> Некоторое число, уникальный номер производителя в БД </param>
        [HttpGet("{id}")]
        public async Task<ActionResult<Manufacturer>> GetManufacturerAsync(int id)
        {
            var manufacturer = await _db.GetManufacturerAsync(id);
            if (manufacturer == null)
                return NotFound();
            return manufacturer;
        }

        /// <summary>
        /// Метод, Добавляющий нового производителя в БД
        /// </summary>
        /// <param name="manufacturer"> Объект производителя </param>
        [HttpPost]
        public async Task<ActionResult<Car>> PostManufacturerAsync(Manufacturer manufacturer)
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