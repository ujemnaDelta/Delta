using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PortalApp.API.DataTransferObjects;

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
    }
}