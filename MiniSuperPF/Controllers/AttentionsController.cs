using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSuperPF.Models;
using MiniSuperPF.Attributes;

namespace MiniSuperPF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class AttentionsController : ControllerBase
    {
        private readonly BD_MiniSuperContext _context;

        public AttentionsController(BD_MiniSuperContext context)
        {
            _context = context;
        }

        // GET: api/Attentions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attention>>> GetAttentions()
        {
          if (_context.Attentions == null)
          {
              return NotFound();
          }
            return await _context.Attentions.ToListAsync();
        }

        // GET: api/Attentions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Attention>> GetAttention(int id)
        {
          if (_context.Attentions == null)
          {
              return NotFound();
          }
            var attention = await _context.Attentions.FindAsync(id);

            if (attention == null)
            {
                return NotFound();
            }

            return attention;
        }

        // PUT: api/Attentions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttention(int id, Attention attention)
        {
            if (id != attention.AttentionId)
            {
                return BadRequest();
            }

            _context.Entry(attention).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttentionExists(id))
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

        // POST: api/Attentions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Attention>> PostAttention(Attention attention)
        {
          if (_context.Attentions == null)
          {
              return Problem("Entity set 'BD_MiniSuperContext.Attentions'  is null.");
          }
            _context.Attentions.Add(attention);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAttention", new { id = attention.AttentionId }, attention);
        }

        // DELETE: api/Attentions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttention(int id)
        {
            if (_context.Attentions == null)
            {
                return NotFound();
            }
            var attention = await _context.Attentions.FindAsync(id);
            if (attention == null)
            {
                return NotFound();
            }

            _context.Attentions.Remove(attention);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AttentionExists(int id)
        {
            return (_context.Attentions?.Any(e => e.AttentionId == id)).GetValueOrDefault();
        }
    }
}
