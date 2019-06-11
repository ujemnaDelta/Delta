using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PortalApp.API.DataTransferObjects;
using PortalApp.API.Models;

namespace PortalApp.API.Data
{
    public class AdminRepository : IAdminPanelRepository
    {
        private readonly DataContext _context;
         private readonly UserManager<UserModel> _userManager;
        public AdminRepository(DataContext context, UserManager<UserModel> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async void AddTeam(Team team)
        {
            await _context.Team.AddAsync(team);
        }

        public async void AddUserTeam(UserTeam team)
        {
             await _context.UserTeam.AddAsync(team);
           
        }

        public async Task<List<string>> AllRoles()
        {
            var roles = await _context.Roles
                                        .Select(p => p.Name).ToListAsync();
            return roles;
        }

        public async Task<List<string>> AllTeams()
        {
            var team = await _context.Team.Select(p => p.NameOfTeam).ToListAsync();
            return team;
        }

        public Task<object> AllUsersWithRoles()
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> CheckTeamUser(UserModel user)
        {
            var result = await _context.UserTeam.AnyAsync(x=> x.UserId == user.Id);
            if(result == false) {
                return false;
            }
            else {
                return true;
            }
        }

        public async Task<IdentityResult> DeleteFromRoles(string UserName, RoleEditDto roleEditDto)
        {
            var user = await _userManager.FindByNameAsync(UserName);

            var userRoles = await _userManager.GetRolesAsync(user);

            var selectedRoles = roleEditDto.RoleNames;
            selectedRoles = selectedRoles ?? new string[] { };

            var result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            return result;
        }

        public async Task<IdentityResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);

            var removeRole = await _userManager.RemoveFromRoleAsync(user,roles.First());
           
            var deleteUser = await _userManager.DeleteAsync(user);

            return deleteUser;
        }

        public async Task<IdentityResult> EditRoles(string UserName, RoleEditDto roleEditDto)
        {
            var user = await _userManager.FindByNameAsync(UserName);

            var userRoles = await _userManager.GetRolesAsync(user);

            var selectedRoles = roleEditDto.RoleNames;
            selectedRoles = selectedRoles ?? new string[] { };
            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));
            return result;
        }

        public async Task<Team> GetTeamAsync(UserForTeamDto userTeamDto)
        {
            var team = await _context.Team.FirstOrDefaultAsync(x => x.NameOfTeam == userTeamDto.UserTeam);

            return team;
        }

        public async Task<UserModel> GetUserAsync(UserForTeamDto userTeamDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userTeamDto.UserLogin);

            return user;
        }

        public async Task<IList<string>> GetUserRoles(string UserName)
        {
            var user = await _userManager.FindByNameAsync(UserName);
            var result = await _userManager.GetRolesAsync(user);
       
            return result;
        }


    }
}