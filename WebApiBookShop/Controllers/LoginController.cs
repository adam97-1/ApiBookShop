using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiBookShop.Models;
using WebApiBookShop.Repositories;

namespace WebApiBookShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IClientRepository _repository;
        public LoginController(IConfiguration configuration, IClientRepository repository)
        {
            _configuration = configuration;
            _repository = repository;
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            var user = Authencate(userLogin);
            if (user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }
            return NotFound("User not found");
        }
        private string Generate(Client user)
        {
            var securityKey = new
            SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey,
            SecurityAlgorithms.HmacSha256);
            var clams = new[] 
            {
                new Claim(ClaimTypes.NameIdentifier, user.ClientId.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            clams,
            expires: DateTime.Now.AddMinutes(20),
            signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private Client? Authencate(UserLogin userLogin)
        {
            var foundUser = _repository.GetClients().FirstOrDefault(
            o => o.Login == userLogin.Login &&
            o.Password == userLogin.Password);
            if (foundUser != null) { return foundUser; }
            else return null;
        }
    }
}
