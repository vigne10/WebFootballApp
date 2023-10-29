using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebFootballApp.Common;
using WebFootballApp.Responses;

namespace WebFootballApp.Auth;

[ApiController]
[Route("[controller]")]
public class AuthController : ApiControllerBase
{
    private readonly AuthServices _authenticationService;
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration, AuthServices authServices)
    {
        _configuration = configuration;
        _authenticationService = authServices;
    }

    [AllowAnonymous]
    [HttpPost("/api/auth/login")]
    public IActionResult Login([FromBody] UserLoginRequest model)
    {
        // TODO: Validate user credentials from your database or authentication system
        var user = _authenticationService.ValidateUser(model.Mail, model.Password);

        if (user == null) return Unauthorized();

        var token = GenerateJwtToken(user);
        return Ok(new { Token = token });
    }

    private string GenerateJwtToken(UserResponse user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Mail),
            new(ClaimTypes.Role, user.Role)
            // Add more claims if needed
        };

        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}