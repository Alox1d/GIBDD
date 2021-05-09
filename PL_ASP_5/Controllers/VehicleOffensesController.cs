 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;

namespace PL_ASP_5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleOffensesController : ControllerBase
    {
        private readonly GIBDDContext _context;

        public VehicleOffensesController(GIBDDContext context)
        {
            _context = context;
        }

        // GET: api/VehicleOffenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleOffense>>> GetVehicleOffenses()
        {
            return await _context.VehicleOffenses
                .Include(s=>s.ArticleOffense)
                .ToListAsync();
        }

        // GET: api/VehicleOffenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleOffense>> GetVehicleOffense(int id)
        {
            var vehicleOffense = await _context.VehicleOffenses.FindAsync(id);

            if (vehicleOffense == null)
            {
                return NotFound();
            }

            return vehicleOffense;
        }
        [Authorize(Roles = "inspector")]

        // PUT: api/VehicleOffenses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicleOffense(int id, VehicleOffense vehicleOffense)
        {
            if (id != vehicleOffense.Id)
            {
                return BadRequest();
            }

            _context.Entry(vehicleOffense).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleOffenseExists(id))
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
        [Authorize(Roles = "inspector")]

        // POST: api/VehicleOffenses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VehicleOffense>> PostVehicleOffense(VehicleOffense vehicleOffense)
        {
            _context.VehicleOffenses.Add(vehicleOffense);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicleOffense", new { id = vehicleOffense.Id }, vehicleOffense);
        }
        [Authorize(Roles = "inspector")]

        // DELETE: api/VehicleOffenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleOffense(int id)
        {
            var vehicleOffense = await _context.VehicleOffenses.FindAsync(id);
            if (vehicleOffense == null)
            {
                return NotFound();
            }

            _context.VehicleOffenses.Remove(vehicleOffense);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VehicleOffenseExists(int id)
        {
            return _context.VehicleOffenses.Any(e => e.Id == id);
        }
    }
}
