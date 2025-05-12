using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repository.Interface;
using Repository.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogRestApiDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;

        public AuthController(IConfiguration configuration, IUserRepository userRepository)
        {
            _config = configuration;
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            if (user != null)
            {
                var userInfo = await _userRepository.GetUserAysnc(user.UserName, user.Password);

                if (userInfo == null) return Unauthorized();

                var cliams = new[]
                {
                    new Claim (ClaimTypes.Name, user.UserName),
                    new Claim (ClaimTypes.Role, user.Role)
                };
                // encode the key given in the config
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
                // use hmacsha algorithm to create creds#
                var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

                //generate token with key and creds, claims

                var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Audience"],
                    signingCredentials: creds,
                    expires: DateTime.UtcNow.AddHours(1),
                    claims: cliams);

                return Ok(new {token = new JwtSecurityTokenHandler().WriteToken(token)});
            }
            else return Unauthorized();

        }
    }
}
