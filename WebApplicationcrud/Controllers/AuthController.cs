using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApplicationcrud.Data;
using WebApplicationcrud.Models;
using WebApplicationcrud.Models.Entities;

namespace WebApplicationcrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        private readonly IConfiguration _config;

        public AuthController(AppDbContext appDbContext, IConfiguration config) {
            this.appDbContext = appDbContext;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto userData)
        {
            Console.WriteLine(userData);
            //CreatePasswordHash(auth.Password, out byte[] hash);
            var user = new Emp()
            {
                Username = userData.Username,
                Email = userData.Email,
                Password = userData.Password,
            };

            appDbContext.emps.Add(user);
            await appDbContext.SaveChangesAsync();
            return Ok(user);


        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto userData)
        {
            var user = await appDbContext.emps.FirstOrDefaultAsync(x => x.Email == userData.Email);

            if (user == null || user.Password != userData.Password)
            {
                return BadRequest("Invalid Data");
            }

            string token = CreateToken(user);

            return Ok(new { token, user });

        }

        private void CreatePasswordHash(string password, out byte[] hash)
        {
            using var hmac = new HMACSHA512();
            hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            
        }

        private bool VerifyPassword(string password, byte[] hash, byte[] salt)
        {
            using var hmac = new HMACSHA512(salt);
            var computed = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computed.SequenceEqual(hash);
        }

        private string CreateToken(Emp user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
