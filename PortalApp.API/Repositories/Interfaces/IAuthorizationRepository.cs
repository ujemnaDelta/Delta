using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PortalApp.API.DataTransferObjects;
using PortalApp.API.Models;

namespace PortalApp.API.Repositories.Interfaces
{
    public interface IAuthorizationRepository
    {
         Task<IdentityResult> RegisterUser(UserForRegisterDTO userForRegisterDTO);
         Task<UserModel> LoginUser(string userName, string userPassword);
         Task<bool> IfUserExists(string userName);

    }
}