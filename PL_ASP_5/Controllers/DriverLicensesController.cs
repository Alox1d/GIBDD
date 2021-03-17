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
    public class DriverLicensesController : ControllerBase
    {
        private readonly GIBDDContext _context;

        public DriverLicensesController(GIBDDContext context)
        {
            _context = context;
        }

        // GET: api/DriverLicenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DriverLicense>>> GetDriverLicenses()
        {
            return await _context.DriverLicenses.ToListAsync();
        }

        // GET: api/DriverLicenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DriverLicense>> GetDriverLicense(int id)
        {
            var driverLicense = await _context.DriverLicenses.FindAsync(id);

            if (driverLicense == null)
            {
                return NotFound();
            }

            return driverLicense;
        }

        // PUT: api/DriverLicenses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDriverLicense(int id, DriverLicense driverLicense)
        {
            if (id != driverLicense.Id)
            {
                return BadRequest();
            }

            _context.Entry(driverLicense).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DriverLicenseExists(id))
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

        // POST: api/DriverLicenses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DriverLicense>> PostDriverLicense(DriverLicense driverLicense)
        {
            _context.DriverLicenses.Add(driverLicense);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDriverLicense", new { id = driverLicense.Id }, driverLicense);
        }

        // DELETE: api/DriverLicenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDriverLicense(int id)
        {
            var driverLicense = await _context.DriverLicenses.FindAsync(id);
            if (driverLicense == null)
            {
                return NotFound();
            }

            _context.DriverLicenses.Remove(driverLicense);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DriverLicenseExists(int id)
        {
            return _context.DriverLicenses.Any(e => e.Id == id);
        }
    }
}
