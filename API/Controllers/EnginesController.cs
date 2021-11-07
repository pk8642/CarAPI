using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Contexts;
using API.Storages.Car.Model;
using API.Storages.Engine;
using API.Storages.Engine.Model;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Класс контроллера для сущности двигателей
    /// </summary>
    [Route("api/engines")]
    [ApiController]
    public class EnginesController : ControllerBase
    {
        private readonly EngineRepository _db;

        /// <summary>
        /// Конструктор контроллера. При отсутствии данных в БД, добавляет строку по умолчанию
        /// </summary>
        /// <param name="context"> Контекст приложения </param>
        public EnginesController(ApplicationContext context)
        {
            _db = new EngineRepository(context);

            if (context.Engines.Any())
                return;
            context.Engines.Add(new Engine
            {
                Id = 1, Type = "Fuel", Cars = new List<string> { "Corolla" }
            });
            context.SaveChanges();
        }

        /// <summary>
        /// Метод, возвращающий список всех двигателей из БД
        /// </summary>
        [HttpGet]
        public async Task<IEnumerable<Engine>> GetEnginesAsync()
        {
            return await _db.GetEnginesListAsync();
        }

        /// <summary>
        /// Метод, возвращающий экземпляр двигателя из БД по id
        /// </summary>
        /// <param name="id"> Некоторое число, уникальный номер двигателя в БД </param>
        [HttpGet("{id}")]
        public async Task<ActionResult<Engine>> GetEngineAsync(int id)
        {
            var engine = await _db.GetEngineAsync(id);
            if (engine == null)
                return NotFound();
            return engine;
        }

        /// <summary>
        /// Метод, Добавляющий новый двигатель в БД
        /// </summary>
        /// <param name="engine"> Объект двигателя </param>
        [HttpPost]
        public async Task<ActionResult<Car>> PostEngineAsync(Engine engine)
        {
            if (engine == null)
            {
                return BadRequest();
            }

            engine.Cars = new List<string>();
            await _db.CreateAsync(engine);
            await _db.SaveAsync();
            return Ok(engine);
        }
    }
}