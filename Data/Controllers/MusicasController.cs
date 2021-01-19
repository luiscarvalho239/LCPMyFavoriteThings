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
    public class MusicasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MusicasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Música
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Música>>> GetMúsicas()
        {
            return await _context.Músicas.ToListAsync();
        }

        // GET: api/Música/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Música>> GetMúsica(int id)
        {
            var música = await _context.Músicas.FindAsync(id);

            if (música == null)
            {
                return NotFound();
            }

            return música;
        }

        // PUT: api/Música/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMúsica(int id, Música música)
        {
            if (id != música.MusicaId)
            {
                return BadRequest();
            }

            _context.Entry(música).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MúsicaExists(id))
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

        // POST: api/Música
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Música>> PostMúsica(Música música)
        {
            _context.Músicas.Add(música);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMúsica", new { id = música.MusicaId }, música);
        }

        // DELETE: api/Música/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMúsica(int id)
        {
            var música = await _context.Músicas.FindAsync(id);
            if (música == null)
            {
                return NotFound();
            }

            _context.Músicas.Remove(música);
            await _context.SaveChangesAsync();
            ResetAI();

            return NoContent();
        }

        private void ResetAI()
        {
            var entityType = _context.Model.FindEntityType(typeof(Música));
            var tableName = entityType.GetTableName();
            var param = new SqlParameter("@tblname", tableName);
            var idlast = _context.Músicas.OrderByDescending(a => a.MusicaId).FirstOrDefault();
            int rsid = (_context.Músicas.Count() > 0) ? idlast.MusicaId : 0;

            _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT(@tblname, RESEED, " + rsid + ")", param);
        }

        private bool MúsicaExists(int id)
        {
            return _context.Músicas.Any(e => e.MusicaId == id);
        }
    }
}
