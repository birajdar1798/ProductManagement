using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace ProductManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthControllerController : ControllerBase
    {
        private readonly JwtService _jwtService;


        public AuthControllerController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }


        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            // Validate user from database
            var userId = "123";


            var accessToken =
                _jwtService.GenerateAccessToken(userId);


            var refreshToken =
                _jwtService.GenerateRefreshToken();


            // Save refresh token in DB
            // UserId + Token + ExpiryDate


            return Ok(new
            {
                accessToken,
                refreshToken
            });
        }
    }
}
