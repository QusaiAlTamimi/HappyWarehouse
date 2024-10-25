using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using HappyWarehouseAPI.Data;
using HappyWarehouseAPI.Models;

namespace HappyItemAPI.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly DataContext _context;

        public ItemsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Items
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Items.ToListAsync());
        }

        // GET: api/Items/5
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var Item = await _context.Items.FirstOrDefaultAsync(m => m.Id == id);
            if (Item == null)
            {
                return NotFound();
            }

            return Ok(Item);
        }

        // POST: api/Items
        [HttpPost("Create")]
        public async Task<IActionResult> Create([Bind("Id,Email,FullName,Role,Active")] Item Item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(Item);
                    await _context.SaveChangesAsync();
                    return Ok(Item);
                }
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT: api/Items/5
        [HttpPut("Update")]
        public async Task<IActionResult> Update(int id, [Bind("Id,Email,FullName,Role,Active")] Item Item)
        {
            if (id != Item.Id)
            {
                return BadRequest("Item ID mismatch.");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    _context.Update(Item);
                    await _context.SaveChangesAsync();
                    return Ok(Item);
                }
                return BadRequest();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(Item.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE: api/Items/5
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest("Invalid Item ID.");
                }

                var Item = await _context.Items.FindAsync(id);
                if (Item == null)
                {
                    return NotFound();
                }

                _context.Items.Remove(Item);
                await _context.SaveChangesAsync();
                return Ok(Item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}
