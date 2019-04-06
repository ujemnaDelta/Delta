using System.Threading.Tasks;
using PortalApp.API.Models;

namespace PortalApp.API.Data
{
    public interface IAuthorizationRepository
    {
         Task<UserModel> RegisterUser(UserModel userName, string userPassword);
         Task<UserModel> LoginUser(string userName, string userPassword);
         Task<bool> IfUserExists(string userName);

    }
}