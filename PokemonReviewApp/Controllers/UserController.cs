using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PokemonReviewApp.InputDtos;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.OutputDtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
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
                return BadRequest("This username is already taken.");

            var newUser = new User
            {
                Username = request.Username,
                Name = request.Name,
                Surname = request.Surname,
            };

            var createdUser = await _userRepository.Register(newUser, request.Password);

            return Ok(new { message = "User successfully created", user = createdUser.Username });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto request)
        {
            var user = await _userRepository.Login(request.Username, request.Password);

            if (user == null)
                return Unauthorized("Invalid username or password.");

            string token = CreateToken(user);
            var permissions = await _userRepository.GetUserPermissions(user.Id);

            var response = new UserLoginOutputDto
            {
                Token = token,
                Message = "Login successful",
                Username = user.Username,
                Permissions = permissions
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

        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto request)
        {
            var success = await _userRepository.ResetPassword(request.Username, request.NewPassword);

            if (!success)
                return NotFound("User not found or an error occurred while updating the password.");

            return Ok(new { message = "Password successfully updated." });
        }


        [HttpGet("users-with-roles")]
        public async Task<IActionResult> GetUsersWithRoles()
        {
            var users = await _userRepository.GetUsersWithRolesAsync();
            return Ok(users);
        }
    }
}