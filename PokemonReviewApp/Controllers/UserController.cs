using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PokemonReviewApp.InputDtos;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.OutputDtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserInterface _userRepository;
        private readonly IConfiguration _configuration;

        public UserController(IUserInterface userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto request)
        {
            if (await _userRepository.UserExists(request.Username))
                return BadRequest("Bu kullanıcı adı zaten alınmış.");

            var newUser = new User
            {
                Username = request.Username,
                Name = request.Name,
                Surname = request.Surname
            };

            var createdUser = await _userRepository.Register(newUser, request.Password);

            return Ok(new { message = "Kullanıcı başarıyla oluşturuldu", user = createdUser.Username });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto request)
        {
            var user = await _userRepository.Login(request.Username, request.Password);

            if (user == null)
                return Unauthorized("Kullanıcı adı veya şifre hatalı.");

            string token = CreateToken(user);

            var response = new UserLoginOutputDto
            {
                Token = token,
                Message = "Giriş başarılı",
                Username = user.Username
            };

            return Ok(response);
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };

            var secret = _configuration
                .GetSection("AppSettings:Token")
                .Value!;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            var creds = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha512Signature
            );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto request)
        {
            var success = await _userRepository.ResetPassword(request.Username, request.NewPassword);

            if (!success)
                return NotFound("User not found or an error occurred while updating the password.");

            return Ok(new { message = "Password successfully updated." });
        }


    }
}