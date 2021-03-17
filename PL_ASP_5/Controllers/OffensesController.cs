using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Entities;

namespace PL_ASP_5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OffensesController : ControllerBase
    {
        private readonly GIBDDContext _context;

        public OffensesController(GIBDDContext context)
        {
            _context = context;
        }

        // GET: api/Offenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Offense>>> GetOffenses()
        {
            return await _context.Offenses.ToListAsync();
        }

        // GET: api/Offenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Offense>> GetOffense(int id)
        {
            var offense = await _context.Offenses.FindAsync(id);

            if (offense == null)
            {
                return NotFound();
            }

            return offense;
        }

        // PUT: api/Offenses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOffense(int id, Offense offense)
        {
            if (id != offense.Id)
            {
                return BadRequest();
            }

            _context.Entry(offense).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OffenseExists(id))
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

        // POST: api/Offenses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Offense>> PostOffense(Offense offense)
        {
            _context.Offenses.Add(offense);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOffense", new { id = offense.Id }, offense);
        }

        // DELETE: api/Offenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOffense(int id)
        {
            var offense = await _context.Offenses.FindAsync(id);
            if (offense == null)
            {
                return NotFound();
            }

            _context.Offenses.Remove(offense);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OffenseExists(int id)
        {
            return _context.Offenses.Any(e => e.Id == id);
        }
    }
}
