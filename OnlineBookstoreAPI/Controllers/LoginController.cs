using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineBookstoreAPI.Application.Service.UserService;
using OnlineBookstoreAPI.Domain.Models.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineBookstoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        public IConfiguration _configuration;
        private readonly IUserService _userService;

        public LoginController(IConfiguration config, IUserService userService)
        {
            _configuration = config;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(LoginDto loginDto)
        {
            if (loginDto != null)
            {
                var user = await GetUser(loginDto);

                if (user != null)
                {
                    //create claims details based on the user information
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.Username!),
                        new Claim("Password", user.PasswordHash!),
                        new Claim(ClaimTypes.Role,user.Role!.RoleName!)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<UserDto?> GetUser(LoginDto loginDto)
        {
            return await _userService.GetUserDtoForLogin(loginDto);
        }
    }
}
