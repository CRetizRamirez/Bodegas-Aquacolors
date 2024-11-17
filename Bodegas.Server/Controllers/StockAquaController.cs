using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bodegas.Server.Models;

namespace Bodegas.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockAquaController : ControllerBase
    {
        private readonly DbAquacolorsContext _context;

        public StockAquaController(DbAquacolorsContext context)
        {
            _context = context;
        }

        // GET: api/StockAqua
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockAqua>>> GetStockAquas()
        {
            return await _context.StockAquas.ToListAsync();
        }

        // GET: api/StockAqua/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StockAqua>> GetStockAqua(int id)
        {
            var stockAqua = await _context.StockAquas.FindAsync(id);

            if (stockAqua == null)
            {
                return NotFound();
            }

            return stockAqua;
        }

        // PUT: api/StockAqua/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStockAqua(int id, StockAqua stockAqua)
        {
            if (id != stockAqua.IdStock)
            {
                return BadRequest();
            }

            _context.Entry(stockAqua).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockAquaExists(id))
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

        // POST: api/StockAqua
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StockAqua>> PostStockAqua(StockAqua stockAqua)
        {
            _context.StockAquas.Add(stockAqua);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStockAqua", new { id = stockAqua.IdStock }, stockAqua);
        }

        // DELETE: api/StockAqua/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockAqua(int id)
        {
            var stockAqua = await _context.StockAquas.FindAsync(id);
            if (stockAqua == null)
            {
                return NotFound();
            }

            _context.StockAquas.Remove(stockAqua);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StockAquaExists(int id)
        {
            return _context.StockAquas.Any(e => e.IdStock == id);
        }
    }
}
