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
using Microsoft.Extensions.Logging;

namespace PL_ASP_5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarOwnersController : ControllerBase
    {
        private readonly GIBDDContext _context;
        private readonly ILogger<CarOwnersController> _logger;

        public CarOwnersController(GIBDDContext context, ILogger<CarOwnersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/CarOwners
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarOwner>>> GetCarOwners()
        {
            return await _context.CarOwners
                .Include(p => p.Vehicles)
                .AsNoTracking()
                .ToListAsync();
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
        [Authorize(Roles = "inspector")]

        // PUT: api/CarOwners/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarOwner(int id, CarOwner carOwner)
        {
            if (id != carOwner.Id)
            {
                return BadRequest();
            }
            //_context.Entry(carOwner).State = EntityState.Modified;
            _context.CarOwners.Update(carOwner);
            //carOwner.Vehicles.
            //_context.Vehicles.All
            var deleteList = _context.Vehicles.Where(t => !carOwner.Vehicles.Contains(t) && t.CarOwner != null).ToList();

            foreach (var d in deleteList)
            {
                _context.Vehicles.Where(s => s.Id == d.Id).First().CarOwner = null;
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                if (!CarOwnerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError(e.ToString());

                    throw;
                }
            }

            return NoContent();
        }
        [Authorize(Roles = "inspector")]

        // POST: api/CarOwners
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CarOwner>> PostCarOwner(CarOwner carOwner)
        {
            _context.Update(carOwner);
            await _context.SaveChangesAsync();
            //_context.CarOwners.Update(carOwner);
            //await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarOwner", new { id = carOwner.Id }, carOwner);
        }
        [Authorize(Roles = "inspector")]

        // DELETE: api/CarOwners/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarOwner(int id)
        {
            var carOwner = await _context.CarOwners.FindAsync(id)
                ;
            if (carOwner == null)
            {
                return NotFound();
            }
            _context.CarOwners.Where(i => i.Id == id).Load();
            _context.Remove(carOwner);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarOwnerExists(int id)
        {
            return _context.CarOwners.Any(e => e.Id == id);
        }
    }
}
