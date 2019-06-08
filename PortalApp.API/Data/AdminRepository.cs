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

        public async Task<List<string>> AllRoles()
        {
            var roles = await _context.UserRoles.Include(p => p.Role)
                                        .Select(p => p.Role.Name).Distinct().ToListAsync();
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
            
            foreach (var role in roles)
            {
                var removeRole = await _userManager.RemoveFromRoleAsync(user, role);
            }
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

        public async Task<IList<string>> GetUserRoles(string UserName)
        {
            var user = await _userManager.FindByNameAsync(UserName);
            var result = await _userManager.GetRolesAsync(user);
       
            return result;
        }


    }
}