using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevsAndSkill.Model;

namespace DevsAndSkill.Controllers
{
    [Route("desenvolvedores")]
    [ApiController]
    public class DesenvolvedorController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public DesenvolvedorController(DatabaseContext context)
        {
            _context = context;
        }

    
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Desenvolvedor>>> GetDesenvolvedor()
        {
            return await _context.Desenvolvedor
                .Include(p => p.Habilidades)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Desenvolvedor>> GetDesenvolvedor(int id)
        {
            var desenvolvedor = await _context.Desenvolvedor.FindAsync(id);
           
            if (desenvolvedor == null)
            {
                return NotFound();
            }
            
            _context.Entry(desenvolvedor)
                    .Collection(h => h.Habilidades)
                    .Load();
            
            return desenvolvedor;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDesenvolvedor(int id, Desenvolvedor desenvolvedor)
        {
            if (id != desenvolvedor.Id)
            {
                return BadRequest();
            }
            var dev = await _context.Desenvolvedor.FindAsync(id);
            dev.Habilidades = desenvolvedor.Habilidades;
            dev.Nome = desenvolvedor.Nome;
            dev.Email = desenvolvedor.Email;
            dev.Site = desenvolvedor.Site;
            _context.Entry(dev).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DesenvolvedorExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Desenvolvedor>> PostDesenvolvedor(Desenvolvedor desenvolvedor)
        {
            _context.Desenvolvedor.Add(desenvolvedor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDesenvolvedor", new { id = desenvolvedor.Id }, desenvolvedor);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Desenvolvedor>> DeleteDesenvolvedor(int id)
        {
            var desenvolvedor = await _context.Desenvolvedor.FindAsync(id);
            if (desenvolvedor == null)
            {
                return NotFound();
            }
            _context.Desenvolvedor.Include(p => p.Habilidades).ToList();
            _context.Desenvolvedor.Remove(desenvolvedor);
            await _context.SaveChangesAsync();
            return desenvolvedor;
        }

        private bool DesenvolvedorExists(int id)
        {
            return _context.Desenvolvedor.Any(e => e.Id == id);
        }
    }
}
