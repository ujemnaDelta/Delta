using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PortalApp.API.DataTransferObjects;
using PortalApp.API.Models;

namespace PortalApp.API.Repositories.Interfaces
{
    public interface IAdminPanelRepository
    {
        Task<List<string>> AllRoles();
        Task<List<string>> AllTeams();
        Task<IdentityResult> DeleteUser(string id);
        Task<IdentityResult> EditRoles(string userName, RoleEditDto roleEditDto);
        Task<IList<string>> GetUserRoles(string userName);
        Task<IdentityResult> DeleteFromRoles(string userName, RoleEditDto roleEditDto);

        Task<UserModel> GetUserAsync(UserForTeamDto userTeamDto);

        Task<Team> GetTeamAsync(UserForTeamDto userTeamDto);

        void AddUserTeam(UserTeam team);

        Task<bool> CheckTeamUser(UserModel user);

        void AddTeam(Team team);
    }
}