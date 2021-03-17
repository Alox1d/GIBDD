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
    public class CarOwnersController : ControllerBase
    {
        private readonly GIBDDContext _context;

        public CarOwnersController(GIBDDContext context)
        {
            _context = context;
        }

        // GET: api/CarOwners
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarOwner>>> GetCarOwners()
        {
            return await _context.CarOwners.ToListAsync();
        }

        // GET: api/CarOwners/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarOwner>> GetCarOwner(int id)
        {
            var carOwner = await _context.CarOwners.FindAsync(id);

            if (carOwner == null)
            {
                return NotFound();
            }

            return carOwner;
        }

        // PUT: api/CarOwners/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarOwner(int id, CarOwner carOwner)
        {
            if (id != carOwner.Id)
            {
                return BadRequest();
            }

            _context.Entry(carOwner).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarOwnerExists(id))
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

        // POST: api/CarOwners
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CarOwner>> PostCarOwner(CarOwner carOwner)
        {
            _context.CarOwners.Add(carOwner);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarOwner", new { id = carOwner.Id }, carOwner);
        }

        // DELETE: api/CarOwners/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarOwner(int id)
        {
            var carOwner = await _context.CarOwners.FindAsync(id);
            if (carOwner == null)
            {
                return NotFound();
            }

            _context.CarOwners.Remove(carOwner);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarOwnerExists(int id)
        {
            return _context.CarOwners.Any(e => e.Id == id);
        }
    }
}
