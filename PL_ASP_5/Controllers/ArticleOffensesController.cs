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
    public class ArticleOffensesController : ControllerBase
    {
        private readonly GIBDDContext _context;

        public ArticleOffensesController(GIBDDContext context)
        {
            _context = context;
        }

        // GET: api/ArticleOffenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleOffense>>> GetArticleOffenses()
        {
            return await _context.ArticleOffenses
                .AsNoTracking()
                .ToListAsync();
        }

        // GET: api/ArticleOffenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleOffense>> GetArticleOffense(int id)
        {
            var articleOffense = await _context.ArticleOffenses.FindAsync(id);

            if (articleOffense == null)
            {
                return NotFound();
            }

            return articleOffense;
        }
        [Authorize(Roles = "inspector")]

        // PUT: api/ArticleOffenses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticleOffense(int id, ArticleOffense articleOffense)
        {
            if (id != articleOffense.Id)
            {
                return BadRequest();
            }

            _context.Entry(articleOffense).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleOffenseExists(id))
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

        // POST: api/ArticleOffenses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ArticleOffense>> PostArticleOffense(ArticleOffense articleOffense)
        {
            _context.ArticleOffenses.Add(articleOffense);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArticleOffense", new { id = articleOffense.Id }, articleOffense);
        }
        [Authorize(Roles = "inspector")]

        // DELETE: api/ArticleOffenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticleOffense(int id)
        {
            var articleOffense = await _context.ArticleOffenses.FindAsync(id);
            if (articleOffense == null)
            {
                return NotFound();
            }

            _context.ArticleOffenses.Remove(articleOffense);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArticleOffenseExists(int id)
        {
            return _context.ArticleOffenses.Any(e => e.Id == id);
        }
    }
}
