using System.Collections.Generic;
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
        private readonly EngineRepository db;
        public EnginesController(ApplicationContext context)
        {
            db = new EngineRepository(context);

            //if (context.Engines.Any())
            //    return;
            //db.Create(new Engine { Id = 1, Type = "fuel", Cars = new List<string> { "Corolla" } });
            //db.Save();
        }

        //public EnginesController()
        //{
        //    db = new EngineRepository();
        //}

        [HttpGet]
        public async Task<IEnumerable<Engine>> GetEngines()
        {
            return await db.GetEnginesListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Engine>> GetEngine(int id)
        {
            var engine = await db.GetEngineAsync(id);
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
            await db.CreateAsync(engine);
            await db.SaveAsync();
            return Ok(engine);
        }
    }
}