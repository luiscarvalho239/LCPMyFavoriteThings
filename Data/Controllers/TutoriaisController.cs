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
    public class TutoriaisController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TutoriaisController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Tutorials
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tutorial>>> GetTutoriais()
        {
            return await _context.Tutoriais.ToListAsync();
        }

        // GET: api/Tutorials/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tutorial>> GetTutorial(int id)
        {
            var tutorial = await _context.Tutoriais.FindAsync(id);

            if (tutorial == null)
            {
                return NotFound();
            }

            return tutorial;
        }

        // PUT: api/Tutorials/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTutorial(int id, Tutorial tutorial)
        {
            if (id != tutorial.TutorialId)
            {
                return BadRequest();
            }

            _context.Entry(tutorial).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TutorialExists(id))
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

        // POST: api/Tutorials
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tutorial>> PostTutorial(Tutorial tutorial)
        {
            _context.Tutoriais.Add(tutorial);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTutorial", new { id = tutorial.TutorialId }, tutorial);
        }

        // DELETE: api/Tutorials/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTutorial(int id)
        {
            var tutorial = await _context.Tutoriais.FindAsync(id);
            if (tutorial == null)
            {
                return NotFound();
            }

            _context.Tutoriais.Remove(tutorial);
            await _context.SaveChangesAsync();
            ResetAI();

            return NoContent();
        }

        private void ResetAI()
        {
            var entityType = _context.Model.FindEntityType(typeof(Tutorial));
            var tableName = entityType.GetTableName();
            var param = new SqlParameter("@tblname", tableName);
            var idlast = _context.Tutoriais.OrderByDescending(a => a.TutorialId).FirstOrDefault();
            int rsid = (_context.Tutoriais.Count() > 0) ? idlast.TutorialId : 0;

            _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT(@tblname, RESEED, " + rsid + ")", param);
        }

        private bool TutorialExists(int id)
        {
            return _context.Tutoriais.Any(e => e.TutorialId == id);
        }
    }
}
