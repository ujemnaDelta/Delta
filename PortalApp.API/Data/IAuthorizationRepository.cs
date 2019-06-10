using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PortalApp.API.DataTransferObjects;
using PortalApp.API.Models;

namespace PortalApp.API.Data
{
    public interface IAuthorizationRepository
    {
         Task<IdentityResult> RegisterUser(UserForRegisterDTO UserForRegisterDTO);
         Task<UserModel> LoginUser(string userName, string userPassword);
         Task<bool> IfUserExists(string userName);

    }
}