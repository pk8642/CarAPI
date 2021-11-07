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
    [Route("api/engines")]
    [ApiController]
    public class EnginesController : ControllerBase
    {
        private readonly EngineRepository _db;
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

        //public EnginesController()
        //{
        //    db = new EngineRepository();
        //}

        [HttpGet]
        public async Task<IEnumerable<Engine>> GetEngines()
        {
            return await _db.GetEnginesListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Engine>> GetEngine(int id)
        {
            var engine = await _db.GetEngineAsync(id);
            if (engine == null)
                return NotFound();
            return engine;
        }


        [HttpPost]
        public async Task<ActionResult<Car>> PostEngine(Engine engine)
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