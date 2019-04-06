using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PortalApp.API.Data;
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
        public async Task<IActionResult> Register(string userName, string userPassword){
            
            userName = userName.ToLower();

            if(await repositoryGlobalField.IfUserExists(userName)){
                return BadRequest("UserName already exists");
            }

            UserModel newUser = new UserModel{UserName = userName};

            UserModel createdUser = await repositoryGlobalField.RegisterUser(newUser, userPassword);

            return null;
        }
















    }
}