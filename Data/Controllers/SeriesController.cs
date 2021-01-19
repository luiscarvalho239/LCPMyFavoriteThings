using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyFavoriteThings.Data.Models;

namespace MyFavoriteThings.Data.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SeriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Série
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Série>>> GetSéries()
        {
            return await _context.Séries.ToListAsync();
        }

        // GET: api/Série/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Série>> GetSérie(int id)
        {
            var série = await _context.Séries.FindAsync(id);

            if (série == null)
            {
                return NotFound();
            }

            return série;
        }

        // PUT: api/Série/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSérie(int id, Série série)
        {
            if (id != série.SerieId)
            {
                return BadRequest();
            }

            _context.Entry(série).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SérieExists(id))
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

        // POST: api/Série
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Série>> PostSérie(Série série)
        {
            _context.Séries.Add(série);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSérie", new { id = série.SerieId }, série);
        }

        // DELETE: api/Série/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSérie(int id)
        {
            var série = await _context.Séries.FindAsync(id);
            if (série == null)
            {
                return NotFound();
            }

            _context.Séries.Remove(série);
            await _context.SaveChangesAsync();
            ResetAI();

            return NoContent();
        }

        private void ResetAI()
        {
            var entityType = _context.Model.FindEntityType(typeof(Série));
            var tableName = entityType.GetTableName();
            var param = new SqlParameter("@tblname", tableName);
            var idlast = _context.Séries.OrderByDescending(a => a.SerieId).FirstOrDefault();
            int rsid = (_context.Séries.Count() > 0) ? idlast.SerieId : 0;

            _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT(@tblname, RESEED, " + rsid + ")", param);
        }

        private bool SérieExists(int id)
        {
            return _context.Séries.Any(e => e.SerieId == id);
        }
    }
}
