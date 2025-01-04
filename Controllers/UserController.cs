using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NotesApp.Services;

namespace NotesApp.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IConfiguration _configuration, IUserService _userService)
    : ControllerBase
{
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IUserResponse>> GetUser()
    {
        var user = await _userService.ValidateAndGetUserDetails(User);
        return Ok(
            new
            {
                user.Id,
                user.Name,
                user.Email,
            }
        );
    }

    [HttpPost("login")]
    public async Task<ActionResult<ILoginResponse>> GenerateTokenAsync(
        [FromBody] LoginRequest request
    )
    {
        // Validate email and password
        var user = await _userService.GetUserByCredentialsAsync(request.Email, request.Password);

        if (user == null)
        {
            return Unauthorized("Invalid email or password.");
        }

        // Generate JWT token
        var jwtKey =
            _configuration["Jwt:Key"] ?? "aLongAndStrongSecretKeyThatIsAtLeast32BytesLong123!";
        var jwtIssuer = _configuration["Jwt:Issuer"] ?? "YourIssuer";

        var claims = new[]
        {
            new Claim("id", user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtIssuer,
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds
        );

        return Ok(
            new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
            }
        );
    }

    public interface IUserResponse
    {
        int Id { get; }
        string? Name { get; }
        string Email { get; }
    }

    public class LoginRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public interface ILoginResponse
    {
        string Token { get; }
        DateTime Expiration { get; }
    }
}
