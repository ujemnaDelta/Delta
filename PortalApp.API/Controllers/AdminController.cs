using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalApp.API.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PortalApp.API.DataTransferObjects;
using Microsoft.AspNetCore.Identity;
using PortalApp.API.Models;
using PortalApp.API.Repositories.Interfaces;

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

        [Authorize(Policy = "RequireHrAdmin")]
        [HttpGet("userswithroles")]
        public async Task<IActionResult> GetUsersWithRoles()
        {
            var userList = await (from user in _context.Users
                                  orderby user.Id
                                 select new
                                 {
                                      Id = user.Id,
                                      UserName = user.UserName,
                                      FullUserName = user.FullUserName,
                                      Position = user.Position,
                                      Team = _context.UserTeam.Include(p => p.Team)
                                        .Where(p => p.UserId == user.Id).Select(p => p.Team.NameOfTeam),
                                    LeaderId = _context.UserTeam.Include(p => p.Team)
                                        .Where(p => p.UserId == user.Id).Select(p => p.Team.LeaderId).FirstOrDefault(),
                             Roles = (from userRole in user.UserRoles
                                         join role in _context.Roles
                                             on userRole.RoleId
                                              equals role.Id
                                               select role.Name).ToList()
                                  }).ToListAsync();
            return Ok(userList);
        }
        [Authorize(Policy = "RequireHrAdmin")]
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

        [Authorize(Policy = "RequireHrAdmin")]
        [HttpGet("teams")]
        public async Task<IActionResult> Teams()
        {
            var teams = await _adminRepo.AllTeams();
            return Ok(teams);
        }
        
        [Authorize(Policy = "RequireHrAdmin")]
        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {

            var roles = await _adminRepo.AllRoles();
            return Ok(roles);
        }

        [Authorize(Policy = "RequireHrAdmin")]
        [HttpGet("teamsmanagment")]
        public async Task<IActionResult> GetTeamsWithLeaders() {

            //var teamReturned = await _context.Team.Select(team => new
            //{
            //    team.Id,
            //    Team = team.NameOfTeam,
            //    team.LeaderId,
            //    LeaderName =
            //        _context.UserTeam.Include(p => p.User)
            //            .Where(p => p.UserId == team.LeaderId)
            //            .Select(p => p.User.FullUserName)
            //            .FirstOrDefault(),
            //    TeamMates = _context.UserTeam.Include(p => p.User)
            //        .Where(p => p.TeamId == team.Id)
            //        .Select(p => p.User.FullUserName)
            //        .ToList()
            //}).ToListAsync();

            return Ok();
        }

        [Authorize(Policy = "RequireAdmin")]
        [HttpPost("userteam")]
        public async Task<IActionResult> AddUserToTeam(UserForTeamDto userTeamDto)
        {
            var user = await _adminRepo.GetUserAsync(userTeamDto);
            var team = await _adminRepo.GetTeamAsync(userTeamDto);

            if(user == null || team == null) {

             return BadRequest("Nie ma takiego użytkownika lub zespołu");

            }
            if(await _adminRepo.CheckTeamUser(user)){
                return BadRequest("Ten użytkownik już ma swój team. Zmień jego team na panelu użytkownika");
            }
            UserTeam userTeam = new UserTeam() {
                TeamId = team.Id,
                UserId = user.Id
            };

             _adminRepo.AddUserTeam(userTeam);
            return StatusCode(201);
        }

        [Authorize(Policy = "RequireAdmin")]
        [HttpDelete("team/{id}")]
        public async Task<IActionResult> DeleteTeam(string id)
        {
            if (id == null || id == "0")
            {
                return BadRequest("Złe id");
            }
            int teamId = int.Parse(id);
            var team = await _context.Team.FirstOrDefaultAsync(x=> x.Id == teamId);
            var userTeam = await _context.UserTeam.Where( x=> x.TeamId == team.Id).ToListAsync();

            if(userTeam == null || team == null) {
                return BadRequest("Nie znalazłem zespołu o takim id lub nie istnieje taka osoba");
            }

            foreach (var user in userTeam)
            {
               _context.UserTeam.Remove(user);
                
            }
            _context.Remove(team);
            await _context.SaveChangesAsync();
            return Ok();
           
        }

        [Authorize(Policy = "RequireAdmin")]
        [HttpGet("userslogin/{username}")]
        public async Task<IActionResult> GetUserLoginByName(string username)
        {

            var user = await _context.Users.Where(x=> x.FullUserName == username).ToListAsync();
            if(user == null) {
                BadRequest("Brak takich użytkowników");
            }
            return Ok(user);
        }

        [Authorize(Policy = "RequireAdmin")]
        [HttpPost("teams")]
        public async Task<IActionResult> CreateTeam(TeamForTeamsDto team)
        {
            if(team == null || team.TeamName == " " || team.TeamName == "") {
                return BadRequest("Nie można stworzyć zespołu bez nazwy");
            }
            if(await _context.Team.AnyAsync(a=> a.NameOfTeam == team.TeamName)){
                return BadRequest("Taki zespoł już istnieje");
            }
           
            var Team = new Team() {
                NameOfTeam = team.TeamName,
              
            };

              _adminRepo.AddTeam(Team);
             await _context.SaveChangesAsync();
            
            return StatusCode(201);
        }

        [Authorize(Policy = "RequireHrAdmin")]
        [HttpGet("leader/{name}")]
        public async Task<IActionResult> GetLeader(string name)
        {
            if(name == null || name.Trim() == "") {
                return BadRequest("Brak nazwy teamu");
            }
            var result = await _context.Team.FirstOrDefaultAsync( x=> x.NameOfTeam == name);
            
            return Ok(result);
        }
    }
}