using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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


        public AuthorizationController(IAuthorizationRepository repository)
        {
            repositoryGlobalField=repository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDTO userForRegister){
            
            userForRegister.UserName = userForRegister.UserName.ToLower();

            if(await repositoryGlobalField.IfUserExists(userForRegister.UserName)){
                return BadRequest("UserName already exists");
            }

            UserModel newUser = new UserModel{UserName = userForRegister.UserName};

            UserModel createdUser = await repositoryGlobalField.RegisterUser(newUser, userForRegister.UserPassword);

            //return CreatedAtRoute();
            
            return StatusCode(201);
        }

    }
}