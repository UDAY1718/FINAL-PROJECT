using AutoMapper;
using Film_Management_System_API.DataModels;
using Film_Management_System_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Film_Management_System_API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        
        public static Admin admin=new Admin();
        private readonly MoviesContext _moviesContext;
        private readonly IMapper mapper;
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration, MoviesContext moviesContext, IMapper mapper)
        {
            this.mapper = mapper;
            _configuration = configuration;
            _moviesContext = moviesContext;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<AdminDTO>> Register(AdminDTO request)
        {
            var user = mapper.Map<Admin>(request);
            await _moviesContext.Admins.AddAsync(user);
            await _moviesContext.SaveChangesAsync();
            CreatePasswordHash(request.Admin_password,out byte[] passwordHash,out byte[] passwordSalt);
            admin.AdminUsernameEmail = request.AdminUsernameEmail;
            admin.AdminPassword = request.Admin_password;
            admin.AdminPasswordHash = passwordHash;
            admin.AdminPasswordSalt = passwordSalt;

            return Ok(admin);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(AdminDTO request)
        {
            if (admin.AdminUsernameEmail != request.AdminUsernameEmail)
            {
                return BadRequest("Admin not found.");
            }
            if (!VerifyPasswordHash(request.Admin_password, admin.AdminPasswordHash, admin.AdminPasswordSalt))
            {
                return BadRequest("Wrong Password");
            }
            string token = CreateToken(admin);
            return Ok(token);
        }

        private string CreateToken(Admin admin)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,admin.AdminUsernameEmail)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);
            var jwt=new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        private void CreatePasswordHash(string password,out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac=new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(admin.AdminPasswordSalt))
            {
                var computedHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
