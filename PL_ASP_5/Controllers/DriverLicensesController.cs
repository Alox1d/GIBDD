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
using BLL.Services;

namespace PL_ASP_5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverLicensesController : ControllerBase
    {
        private readonly GIBDDContext _context;
        private DriverLicenseService _driverLicenseService;

        public DriverLicensesController(GIBDDContext context, DriverLicenseService driverLicenseService)
        {
            _context = context;
            _driverLicenseService = driverLicenseService;
        }
        [HttpGet]
        [Route("LicenseCheck")]
        public int GetLicenseCheck()
        { 
            int count = _driverLicenseService
                .CheckLicense(_context.DriverLicenses
                .Include(s=>s.TakeStrokes)
                .ToList());
            _context.SaveChanges();

            return count;
        }
        // GET: api/DriverLicenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DriverLicense>>> GetDriverLicenses()
        {
            return await _context.DriverLicenses
                .Include(s=>s.CarOwner)
                .Include(s=>s.TakeStrokes)
                .Include(s=>s.Categories)
                .AsNoTracking()
                .ToListAsync();
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
        [Authorize(Roles = "inspector")]

        // PUT: api/DriverLicenses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDriverLicense(int id, DriverLicense driverLicense)
        {
            if (id != driverLicense.Id)
            {
                return BadRequest();
            }
            //_context.Update(driverLicense);
            //_context.SaveChanges();

            //_context.Attach(driverLicense);
            //_context.Entry(driverLicense).State = EntityState.Modified;
            //foreach (var item in driverLicense.Categories)
            //{
            //    _context.Entry(item).State = EntityState.Added;
            //}
            //driverLicense.Categories.ForEach(a => driverLicense.ActivitityTravels.Add(new ActivityTravel() { Activity = a, Travel = travel }));
            //_context.AttachRange(driverLicense.Categories);
            //_context.DriverLicenses.Update(driverLicense);
            //var newCategories = _context.Categories.Where(t => !driverLicense.Categories.Contains(t)).ToList();
            //foreach (var n in newCategories)
            //{
            //    driverLicense.Categories = newCategories;
            //}
            _context.Attach(driverLicense);

            var deleteList = _context.TakeStrokes.Where(t => !driverLicense.TakeStrokes.Contains(t) && t.DriverLicense != null).ToList();
            foreach (var d in deleteList)
            {
                _context.TakeStrokes.Where(s => s.Id == d.Id).First().DriverLicense = null;
            }

            var deleteListCategories = _context.Categories.Where(t => !driverLicense.Categories.Contains(t) && t.DriverLicenses.Count != 0).ToList();
            foreach (var d in deleteListCategories)
            {
                var foundLicense = _context.DriverLicenses.Where(s => s.Id == driverLicense.Id)
                    .Include(s=>s.Categories)
                    .First();
                foundLicense.Categories.Remove(d);
                //_context.Categories.Where(s => s.Id == d.Id).First().DriverLicenses.ForEach(t=>{
                //    if (t.Id == driverLicense.Id) t.= null;
            }

            foreach (var item in driverLicense.Categories)
            {
                var cats = _context.DriverLicenses.Where(t => t.Id == id && !t.Categories.Contains(item)).ToList();
                if (cats.Count !=0)
                _context.Entry(item).State = EntityState.Added;
            }
            _context.Update(driverLicense);
            try
            {
                _context.SaveChanges();
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
        [Authorize(Roles = "inspector")]

        // POST: api/DriverLicenses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DriverLicense>> PostDriverLicense(DriverLicense driverLicense)
        {
            //_context.DriverLicenses.Add(driverLicense);
            _context.Update(driverLicense);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDriverLicense", new { id = driverLicense.Id }, driverLicense);
        }
        [Authorize(Roles = "inspector")]

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
