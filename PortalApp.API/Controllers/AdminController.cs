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
        private readonly IAdminPanelRepository _adminRepo;
        private readonly DataContext _context;
        private readonly UserManager<UserModel> _userManager;

        public AdminController(DataContext context, UserManager<UserModel> userManager, IAdminPanelRepository adminRepo)
        {
            _context = context;
            _userManager = userManager;
            _adminRepo = adminRepo;
        }

        [Authorize(Policy = "RequireAdmin")]
        [HttpGet("userswithroles")]
        public async Task<IActionResult> GetUsersWithRoles()
        {
            var asd = await _context.Users.FirstOrDefaultAsync();

            var userList = await (from user in _context.Users
                                  orderby user.UserName
                                  select new
                                  {
                                      Id = user.Id,
                                      UserName = user.UserName,
                                      FullUserName = user.FullUserName,
                                      Team = _context.UserTeam.Include(p => p.Team)
                                        .Where(p => p.UserId == user.Id).Select(p => p.Team.NameOfTeam),
                                      Roles = (from userRole in user.UserRoles
                                               join role in _context.Roles
                                               on userRole.RoleId
                                               equals role.Id
                                               select role.Name).ToList()
                                  }).ToListAsync();
            return Ok(userList);
        }
        [Authorize(Policy = "RequireAdmin")]
        [HttpPost("editRoles/{UserName}")]
        public async Task<IActionResult> EditRoles(string UserName, RoleEditDto roleEditDto)
        {
            var result = await _adminRepo.EditRoles(UserName,roleEditDto);

            if (!result.Succeeded)
            {
                return BadRequest("Failed to add to roles");
            }

            result = await _adminRepo.DeleteFromRoles(UserName,roleEditDto);
            if (!result.Succeeded)
            {
                return BadRequest("Failed to remove role");
            }

                var userToReturned = await _adminRepo.GetUserRoles(UserName);
            return Ok(userToReturned);

        }

        [Authorize(Policy = "RequireAdmin")]
        [HttpDelete("user/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (id == null || id == "0")
            {
                return BadRequest("Bad Id");
            }
            
            var delete = await _adminRepo.DeleteUser(id);
            if(delete.Succeeded) {
                return Ok();
            }
            else {
                return BadRequest("Delete not sucessfulled");
            }
        }

        [Authorize(Policy = "RequireAdmin")]
        [HttpGet("teams")]
        public async Task<IActionResult> Teams()
        {
            var teams = await _adminRepo.AllTeams();
            return Ok(teams);
        }

        [Authorize(Policy = "RequireAdmin")]
        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {

            var roles = await _adminRepo.AllRoles();
            return Ok(roles);
        }

        [Authorize(Policy = "RequireAdmin")]
        [HttpGet("teamsmanagment")]
        public async Task<IActionResult> GetTeamsWithLeaders() {

            var TeamReturned = await (from team in _context.Team
                                  select new
                                  {
                                      Id = team.Id,
                                      Team = team.NameOfTeam,
                                      LeaderId = team.LeaderId,
                                      LeaderName = _context.UserTeam.Include(p => p.User)
                                        .Where(p => p.UserId == team.LeaderId).Select(p => p.User.FullUserName).FirstOrDefault(),
                                      TeamMates = _context.UserTeam.Include(p => p.User).Where( p => p.TeamId == team.Id).Select(p => p.User.FullUserName).ToList()
                                  }).ToListAsync();

            return Ok(TeamReturned);
        }
    }
}