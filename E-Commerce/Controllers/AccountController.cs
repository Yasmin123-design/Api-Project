using E_Commerce.Dto;
using E_Commerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration configuration;
        public AccountController(UserManager<ApplicationUser> userManager,IConfiguration config)
        {
            _userManager = userManager;
            configuration = config;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto register)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser application = new ApplicationUser();
                application.UserName = register.UserName;
                application.PasswordHash = register.Password;
                application.Email = register.Email;
                IdentityResult result = await _userManager.CreateAsync(application, register.Password);
                if(result.Succeeded)
                {
                    return Ok(application);
                }
                return BadRequest(result.Errors.FirstOrDefault());
            }
            return BadRequest(ModelState);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto login)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByNameAsync(login.UserName);
                if (user != null)
                {
                    bool found = await _userManager.CheckPasswordAsync(user, login.Password);
                    if (found)
                    {
                        // create token
                        List<Claim> claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.PasswordHash));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())); // jti=>unique

                        var roles = await _userManager.GetRolesAsync(user);
                        foreach (var role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role));
                        }
                        
                        SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"])); // key لازم يبقى bytes
                        SigningCredentials signing = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        JwtSecurityToken token = new JwtSecurityToken(
                            issuer: configuration["JWT:ValidIssuer"], // api url
                            audience: configuration["JWT:ValidAudiance"], // consumer url (angular)
                            claims: claims,
                            expires: DateTime.Now.AddHours(1), // token هيبقى valid لحد امتى
                            signingCredentials: signing

                            );

                        // كل اللى فوق دا كنا ب represent token عايزين create token 
                        // anonymous object
                        return Ok(new
                        {
                            mytoken = new JwtSecurityTokenHandler().WriteToken(token), // create token
                            expiration = token.ValidTo  // هتبقى valid لحد امتا
                        });
                    }
                    
                }
                return Unauthorized();
            }
            return BadRequest(ModelState);
        }
    }
}
