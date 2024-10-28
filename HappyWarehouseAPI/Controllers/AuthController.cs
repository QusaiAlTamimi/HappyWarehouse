using Microsoft.AspNetCore.Mvc;
using HappyWarehouseCore.Dtos;
using HappyWarehouseService.IServices;
using Microsoft.AspNetCore.Authorization;
using HappyWarehouseService.Services;

namespace HappyItemAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _authService.GetAllUsersAsync();
            return Ok(users); // Return list of users
        }

        [HttpGet("GetPaged")]
        public async Task<IActionResult> GetPaged(int pageNumber, int pageSize)
        {
            var users = await _authService.GetAllUsersAsync(pageNumber, pageSize);
            return Ok(users);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetUserByIdAsync(string userId)
        {
            var user = await _authService.GetUserByIdAsync(userId);
            if (user == null)
                return NotFound(); // Return 404 if user not found

            return Ok(user); // Return user details
        }

        [HttpPost("Create")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);
            else
            {
                return Ok(model);
            }

            //return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateUser(string userId, [FromBody] RegisterModel model)
        {
            if (model == null)
            {
                return BadRequest("User model is null.");
            }

            var result = await _authService.UpdateAsync(userId, model);
            if (!string.IsNullOrEmpty(result.Message))
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(string userId, [FromBody] ChangePasswordDto model)
        {
            if (model == null)
            {
                return BadRequest("Change password model is null.");
            }

            var result = await _authService.ChangePasswordAsync(userId, model);
            if (!string.IsNullOrEmpty(result.Message))
            {
                return BadRequest(result.Message); // Return error message if any
            }

            return Ok(result); // Return success message
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.GetTokenAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.AddRoleAsync(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(model);
        }
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUserAsync(string userId)
        {
            var result = await _authService.DeleteUserAsync(userId);

            if (result != null)
                return BadRequest(result); // If there's an error message, return it

            return Ok(new { Message = "User deleted successfully." });
        }
    }
}