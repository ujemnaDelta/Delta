using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PortalApp.API.DataTransferObjects;
using PortalApp.API.Models;

namespace PortalApp.API.Data
{
    public interface IAdminPanelRepository
    {
        Task<List<string>> AllRoles();
        Task<List<string>> AllTeams();
        Task<IdentityResult> DeleteUser(string id);
        Task<IdentityResult> EditRoles(string UserName, RoleEditDto roleEditDto);
        Task<IList<string>> GetUserRoles(string UserName);
        Task<IdentityResult> DeleteFromRoles(string UserName, RoleEditDto roleEditDto);

        Task<UserModel> GetUserAsync(UserForTeamDto userTeamDto);

        Task<Team> GetTeamAsync(UserForTeamDto userTeamDto);

        void AddUserTeam(UserTeam team);

        Task<bool> CheckTeamUser(UserModel user);

        void AddTeam(Team team);
    }
}