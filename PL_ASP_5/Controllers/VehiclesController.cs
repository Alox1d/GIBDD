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
using Microsoft.EntityFrameworkCore.ChangeTracking;
using BLL.Services;
using Microsoft.Extensions.Logging;

namespace PL_ASP_5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly GIBDDContext _context;
        private MaintenanceService _maintenanceService;
        private readonly ILogger<VehiclesController> _logger;

        public VehiclesController(GIBDDContext context, 
            MaintenanceService maintenanceService, ILogger<VehiclesController> logger)
        {
            _context = context;
            _maintenanceService = maintenanceService;
            _logger = logger;
        }
        [HttpGet]
        [Route("MaintenanceCheck")]
        public int GetMaintenanceCheck()
        {
            int count = _maintenanceService
                .CheckMaintenance(_context.Vehicles.ToList());
            _context.SaveChanges();

            return count;
        }
        // GET: api/Vehicles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
        {
            return await _context.Vehicles
                .Include(c =>c.Color)
                .Include(m => m.Model)
                .Include(c => c.Category)
                .Include(a=>a.CarOwner)
                    .ThenInclude(s=>s.DriverLicense)
                        .ThenInclude(s=>s.TakeStrokes)
                .AsNoTracking()
                .ToListAsync();
        }

        // GET: api/Vehicles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicle(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return vehicle;
        }

        // PUT: api/Vehicles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "inspector")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicle(int id, Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return BadRequest();
            }

            _context.Update(vehicle);

            //_context.Entry(vehicle).State = EntityState.Modified;


            //IEnumerable<EntityEntry> unchangedEntities = _context.ChangeTracker.Entries().Where(x => x.State == EntityState.Unchanged);

            //foreach (EntityEntry ee in unchangedEntities)
            //{
            //    ee.State = EntityState.Modified;
            //}

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                if (!VehicleExists(id))
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

        // POST: api/Vehicles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "inspector")]
        [HttpPost]
        public async Task<ActionResult<Vehicle>> PostVehicle(Vehicle vehicle)
        {
            //_context.Vehicles.Add(vehicle);
            _context.Update(vehicle);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicle", new { id = vehicle.Id }, vehicle);
        }

        // DELETE: api/Vehicles/5
        [Authorize(Roles = "inspector")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VehicleExists(int id)
        {
            return _context.Vehicles.Any(e => e.Id == id);
        }
    }
}
