using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactorApi.Data;
using ReactorApi.Models;

namespace ReactorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReactorController : ControllerBase
    {
        private readonly DatabaseContext database;

        public ReactorController(DatabaseContext context)
        {
            database = context;
        }

        // GET: api/Reactor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reactor>>> GetReactors()
        {
            return await database.Reactors.ToListAsync();
        }

        // GET: api/Reactor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reactor>> GetReactor(int id)
        {
            var reactor = await database.Reactors.FindAsync(id);

            if (reactor == null)
            {
                return NotFound();
            }

            return reactor;
        }

        // PUT: api/Reactor/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReactor(int id, Reactor reactor)
        {
            if (id != reactor.Id)
            {
                return BadRequest();
            }

            database.Entry(reactor).State = EntityState.Modified;

            try
            {
                await database.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReactorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Reactor
        [HttpPost]
        public async Task<ActionResult<Reactor>> PostReactor(Reactor reactor)
        {
            Console.WriteLine(reactor);
            database.Reactors.Add(reactor);
            await database.SaveChangesAsync();

            return CreatedAtAction("GetReactor", new { id = reactor.Id }, reactor);
        }

        // DELETE: api/Reactor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReactor(int id)
        {
            var reactor = await database.Reactors.FindAsync(id);
            if (reactor == null)
            {
                return NotFound();
            }

            database.Reactors.Remove(reactor);
            await database.SaveChangesAsync();

            return NoContent();
        }

        private bool ReactorExists(int id)
        {
            return database.Reactors.Any(e => e.Id == id);
        }
    }
}
