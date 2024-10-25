//using HappyWarehouseAPI.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System.Net.Http.Headers;

//[ApiController]
//[Route("api/[controller]")]
//public class Auth2Controller : ControllerBase
//{
//    private readonly JwtTokenService _jwtTokenService;

//    public Auth2Controller(JwtTokenService jwtTokenService)
//    {
//        _jwtTokenService = jwtTokenService;
//    }

//    [HttpGet("data")]
//    [Authorize]
//    public IActionResult GetProtectedData()
//    {
//        return Ok(new { Data = "This is protected data" });
//    }

//    [HttpPost("login")]
//    public IActionResult Login([FromBody] LoginRequest request)
//    {
//        // Validate the user credentials here
//        if (request.Email == "admin@happywarehouse.com" && request.Password == "P@ssw0rd") // Simplified for demonstration
//        {
//            var token = _jwtTokenService.GenerateToken("1", request.Email);
//            var client = new HttpClient();
//            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
//            return Ok(new { Token = token });
//        }

//        return Unauthorized();
//    }
//}
