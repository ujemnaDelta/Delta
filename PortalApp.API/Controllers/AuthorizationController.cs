using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PortalApp.API.Data;
using PortalApp.API.DataTransferObjects;
using PortalApp.API.Models;
using PortalApp.API.Repositories.Interfaces;

namespace PortalApp.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationRepository repositoryGlobalField;
        private readonly DataContext _context;
        private readonly IConfiguration configurationGlobalField;
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;

        public AuthorizationController(DataContext context,IConfiguration configuration, UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, IAuthorizationRepository repo)
        {
            configurationGlobalField = configuration;
            repositoryGlobalField = repo;
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        [Authorize(Policy = "RequireHrAdmin")]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDTO userForRegister)
        {

            

            if (await repositoryGlobalField.IfUserExists(userForRegister.UserName))
            {
                return BadRequest("Taki login już istnieje");
            }
            
            var result = await repositoryGlobalField.RegisterUser(userForRegister);
            if(result == null) {
                    return BadRequest("Ten zespół ma już swojego lidera");
                }

            if(result.Succeeded) {
                return StatusCode(201);
            }
            else {
                    return BadRequest("Proces rejestracji nie został wykonany poprawnie");
            }
            
            
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDTO userForLogin)
        {

            var user = await _userManager.FindByNameAsync(userForLogin.UserName);
            if(user == null) {
                return Unauthorized();
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, userForLogin.UserPassword, false);

            if (result.Succeeded)
            {
                var appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == userForLogin.UserName.ToUpper());
                return Ok(new
                {
                    token = GenerateJwtToken(appUser).Result
                });
            }
            return Unauthorized();

        }
        private async Task<string> GenerateJwtToken(UserModel user)
        {
            var claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configurationGlobalField.GetSection("AppSetting:Token").Value));
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configurationGlobalField.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);



            var tokenDescriptor = new SecurityTokenDescriptor
            {
               
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds,
          
            };

            var tokenhandler = new JwtSecurityTokenHandler();
            var token = tokenhandler.CreateToken(tokenDescriptor);
            return tokenhandler.WriteToken(token);
        }
    }
}