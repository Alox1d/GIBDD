using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Entities;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Bll.Models.ReportsModel;
using Microsoft.Extensions.Logging;

namespace PL_ASP_5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OffensesController : ControllerBase
    {
        private readonly GIBDDContext _context;
        private OffenseService _offenseService;
        private readonly ILogger<OffensesController> _logger;
        private ReportsService _reportsService;
        public OffensesController(GIBDDContext context, 
            OffenseService offenseService,
            ReportsService reportsService, ILogger<OffensesController> logger
            )
        {
            _logger = logger;

            _context = context;
            _offenseService = offenseService;
            _reportsService = reportsService; 
        }
        [HttpGet]
        [Route("MakeReport")]
        public List<ArticleSum> MakeReport(DateTime startDate, DateTime endDate)
        {

            return _reportsService
                .MakeReport(_context.VehicleOffenses
                .Include(s => s.CarDriver)
                    .ThenInclude(s => s.Offenses)
                    .Include(s=>s.ArticleOffense)
                    .AsSplitQuery()
                .ToList(), startDate, endDate);;
        }
        // GET: api/Offenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Offense>>> GetOffenses()
        {
            return await _context.Offenses
                .Include(s=>s.CarDriver)
                    .ThenInclude(s=> s.VehicleOffenses)
                    .ThenInclude(s=>s.ArticleOffense)
                .Include(s => s.CarDriver)
                    .ThenInclude(s => s.Vehicle)
                        .ThenInclude(s=>s.CarOwner)
                            .ThenInclude(s=>s.DriverLicense)
                                .ThenInclude(s=>s.TakeStrokes)
                .AsNoTracking()
                .AsSplitQuery()
                .ToListAsync();
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
        [Authorize(Roles = "inspector")]

        // PUT: api/Offenses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOffense(int id, Offense offense)
        {
            if (id != offense.Id)
            {
                return BadRequest();
            }
            _offenseService.CreateOffense(offense);
            //_context.Entry(offense).State = EntityState.Modified;
            _context.Update(offense);
            var deleteList = _context.VehicleOffenses.Where(t => !offense.CarDriver.VehicleOffenses.Contains(t) && t.CarDriver != null).ToList();
            //var deleteList = _context.VehicleOffenses.Where(t => !offense.CarDriver.VehicleOffenses.Exists(x => x.Id == t.Id) && t.CarDriver != null).ToList();

            foreach (var d in deleteList)
            {
                _context.VehicleOffenses.Where(s => s.Id == d.Id).First().CarDriver = null;
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                if (!OffenseExists(id))
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

        // POST: api/Offenses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Offense>> PostOffense(Offense offense)
        {
            
            _offenseService.CreateOffense(offense);
            _context.Update(offense);
            //offense.CarDriver.Vehicle.Color = null;
            //offense.CarDriver.Vehicle.Model = null;
            //offense.CarDriver.Vehicle.Category = null;
            //offense.CarDriver.Vehicle.CarOwner = null;
            ////offense.CarDriver.VehicleOffenses.ForEach(i=>i.)
            ////_context.Entry().State = EntityState.Modified;
            //var v = offense.CarDriver.Vehicle;
            //offense.CarDriver.Vehicle = null;
            //var added = _context.Set<Offense>().Add(offense).Entity;
            //await _context.SaveChangesAsync();
            ////_context.Entry(v).State = EntityState.Modified;
            //offense.CarDriver.Vehicle = v;
            //_context.Entry(added).CurrentValues.SetValues(offense);
            //_context.Offenses.Update(offense);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOffense", new { id = offense.Id }, offense);
        }
        [Authorize(Roles = "inspector")]

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
