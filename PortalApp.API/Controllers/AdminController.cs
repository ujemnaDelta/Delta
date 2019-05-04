using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalApp.API.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PortalApp.API.DataTransferObjects;
using Microsoft.AspNetCore.Identity;
using PortalApp.API.Models;

namespace PortalApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly UserManager<UserModel> _userManager;

        public AdminController(DataContext context, UserManager<UserModel> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [Authorize(Policy = "AdminRole")]
        [HttpGet("userswithroles")]
        public async Task<IActionResult> GetUserWithRoles()
        {
            var userList = await (from user in _context.Users orderby user.UserName
                                    select new {
                                        Id = user.Id,
                                        UserName = user.UserName,
                                        Roles = (from userRole in user.UserRoles join role in _context.Roles on userRole.RoleId equals role.Id select role.Name).ToList()
                                    } ).ToListAsync();
            return Ok("Only admins can see it");
        }
        [Authorize(Policy = "AdminRole")]
        [HttpPost("editRoles/{UserName}")]
        public async Task<IActionResult> EditRoles(string UserName, RoleEditDto roleEditDto) 
        {
            var user = await _userManager.FindByNameAsync(UserName);

            var userRoles = await _userManager.GetRolesAsync(user);

            var selectedRoles = roleEditDto.RoleNames;
            selectedRoles = selectedRoles ?? new string[] {};
            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if(!result.Succeeded){
                return BadRequest("Failed to add to roles");  
            }
            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));
             if(!result.Succeeded){
                return BadRequest("Failed to remove role");  
            }

            return Ok(await _userManager.GetRolesAsync(user));

        }


        [Authorize(Policy = "HRRole")]
        [HttpGet("allUser")]
        public IActionResult GetAllUser()
        {
            return Ok("Only HR and Admin can see it");
        }

        [Authorize(Policy = "LeaderRole")]
        [HttpGet("teamUsers")]
        public IActionResult GetTeamUsers()
        {
            return Ok("Only Leader and Admin can see it");
        }
    }
}