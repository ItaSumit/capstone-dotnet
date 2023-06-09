using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Capstone.Admin.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Capstone.Admin.Controllers;

[ApiController]
[Route("admin/api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<Models.Admin> _userManager;
    private readonly IConfiguration _configuration;

    public AuthController(UserManager<Models.Admin> userManager,
        IConfiguration configuration)
    {
        this._userManager = userManager;
        _configuration = configuration;
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] Login model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);
        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.UserData, user.Id.ToString())
            };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

        return Unauthorized("Not authorised");
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] Register model)
    {
        var userExists = await _userManager.FindByNameAsync(model.Username);
        if (userExists != null)
            return StatusCode(StatusCodes.Status500InternalServerError,
                new Response { Status = "Error", Message = "User already exists!" });

        Models.Admin user = new Models.Admin()
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Username,
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            return StatusCode(StatusCodes.Status500InternalServerError,
                new Response
                    { Status = "Error", Message = "User creation failed! Please check user details and try again." });

        var savedUser = await _userManager.FindByNameAsync(user.UserName);

        return Ok(new Response { Status = "Success", Message = "User created successfully!" });
    }
}