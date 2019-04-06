using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PortalApp.API.Data;
using PortalApp.API.DataTransferObjects;
using PortalApp.API.Models;

namespace PortalApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationRepository repositoryGlobalField;

        private readonly IConfiguration configurationGlobalField;

        public AuthorizationController(IAuthorizationRepository repository, IConfiguration configuration)
        {
            configurationGlobalField = configuration;
            repositoryGlobalField = repository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDTO userForRegister)
        {

            userForRegister.UserName = userForRegister.UserName.ToLower();

            if (await repositoryGlobalField.IfUserExists(userForRegister.UserName))
            {
                return BadRequest("UserName already exists");
            }

            UserModel newUser = new UserModel { UserName = userForRegister.UserName };

            UserModel createdUser = await repositoryGlobalField.RegisterUser(newUser, userForRegister.UserPassword);

            //return CreatedAtRoute();

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDTO userForLogin)
        {
            var userFromRepository = await repositoryGlobalField.LoginUser(userForLogin.UserName, userForLogin.UserPassword);

            if (userFromRepository == null)
            {
                return Unauthorized();
            }

            var claims = new[]{
                new Claim(ClaimTypes.NameIdentifier, userFromRepository.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepository.UserName)
            };

            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configurationGlobalField.GetSection("AppSetting:Token").Value));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenhandler = new JwtSecurityTokenHandler();
            var token = tokenhandler.CreateToken(tokenDescriptor);
            return Ok(new {
                token = tokenhandler.WriteToken(token)
            });

        }
    }
}