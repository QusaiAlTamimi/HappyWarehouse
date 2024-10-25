//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using HappyWarehouseAPI.Data;
//using HappyWarehouseAPI.Models;
//using System.Threading.Tasks;
//using System.Linq;
//using Microsoft.AspNetCore.Authorization;

//namespace HappyWarehouseAPI.Controllers
//{
//    [Route("api/[controller]/")]
//    [ApiController]
//    [Authorize]
//    public class UsersController : ControllerBase
//    {
//        private readonly DataContext _context;

//        public UsersController(DataContext context)
//        {
//            _context = context;
//        }

//        // GET: api/Users
//        [HttpGet("GetAll")]
//        public async Task<IActionResult> GetAll()
//        {
//            return Ok(await _context.Users.ToListAsync());
//        }

//        // GET: api/Users/5
//        [HttpGet("GetById")]
//        public async Task<IActionResult> GetById(int? id)
//        {
//            if (id == null || _context.Users == null)
//            {
//                return NotFound();
//            }

//            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
//            if (user == null)
//            {
//                return NotFound();
//            }

//            return Ok(user);
//        }

//        // POST: api/Users
//        [HttpPost("Create")]
//        public async Task<IActionResult> Create(User user)
//        {
//            try
//            {
//                if (ModelState.IsValid)
//                {
//                    _context.Add(user);
//                    await _context.SaveChangesAsync();
//                    return Ok(user);
//                }
//                else
//                    return BadRequest();
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex);
//            }
//        }

//        // PUT: api/Users/5
//        [HttpPut("Update")]
//        public async Task<IActionResult> Update(User user)
//        {
//            if (user.Id == 0)
//            {
//                return BadRequest("User ID mismatch.");
//            }

//            try
//            {
//                if (ModelState.IsValid)
//                {
//                    _context.Update(user);
//                    await _context.SaveChangesAsync();
//                    return Ok(user);
//                }
//                return BadRequest();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!UserExists(user.Id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex);
//            }
//        }

//        // DELETE: api/Users/5
//        [HttpDelete("Delete")]
//        public async Task<IActionResult> Delete(int id)
//        {
//            try
//            {
//                if (id == 0)
//                {
//                    return BadRequest("Invalid user ID.");
//                }

//                var user = await _context.Users.FindAsync(id);
//                if (user == null)
//                {
//                    return NotFound();
//                }

//                _context.Users.Remove(user);
//                await _context.SaveChangesAsync();
//                return Ok(user);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex);
//            }
//        }

//        private bool UserExists(int id)
//        {
//            return _context.Users.Any(e => e.Id == id);
//        }
//    }
//}
