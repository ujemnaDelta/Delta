using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PortalApp.API.Data;
using PortalApp.API.DataTransferObjects;
using PortalApp.API.Models;
using PortalApp.API.Repositories.Interfaces;

namespace PortalApp.API.Repositories
{
    public class AuthorizationRepository : IAuthorizationRepository
    {
        private readonly DataContext _context;
        private readonly UserManager<UserModel> _userManager;
        public AuthorizationRepository(DataContext context, UserManager<UserModel> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //Automatycznie wygenerowane 
        public async Task<bool> IfUserExists(string userName)
        {
            if (await _context.Users.AnyAsync(x => x.UserName == userName))
            {
                return true;
            }
            return false;
        }

        public async Task<UserModel> LoginUser(string userName, string userPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName);

            if (user == null)
            {
                return null;
            }

            //if(!CheckPassword(userPassword, user.UserPasswordHash, user.UserPasswordSalt)){
            //   return null;
            //}

            return user;
        }

        private bool CheckPassword(string userPassword, byte[] userPasswordHash, byte[] userPasswordSalt)
        {
            //do skasowania potem moi drodzy bo jak będzie nullem to wywali aplikację :*
            if (userPassword == null)
            {
                return false;
            }
            using (var hashMsg = new System.Security.Cryptography.HMACSHA512(userPasswordSalt))
            {

                var calculatedHash = hashMsg.ComputeHash(System.Text.Encoding.UTF8.GetBytes(userPassword));

                for (int i = 0; i < calculatedHash.Length; i++)
                {
                    if (calculatedHash[i] != userPasswordHash[i])
                        return false;
                }
            }
            return true;
        }

        public async Task<IdentityResult> RegisterUser(UserForRegisterDTO userForRegisterDTO)
        {
            if (userForRegisterDTO.roles == "Leader")
            {
                var TeamResult = await _context.Team.FirstOrDefaultAsync(x => x.NameOfTeam == userForRegisterDTO.team);
                if (TeamResult.LeaderId != 0)
                {
                    return null;
                }

            }

            UserModel newUser = new UserModel
            {
                UserName = userForRegisterDTO.UserName,
                FullUserName = userForRegisterDTO.FullUserName,
                Position = userForRegisterDTO.Position
            };

            IdentityResult result = (await _userManager.CreateAsync(newUser, userForRegisterDTO.UserPassword));
            var createdUser = (await _userManager.FindByNameAsync(userForRegisterDTO.UserName));

            var team = await _context.Team.FirstOrDefaultAsync(p => p.NameOfTeam == userForRegisterDTO.team);



            UserTeam createdUserTeam = new UserTeam
            {
                User = createdUser,
                Team = team
            };

            await _context.UserTeam.AddAsync(createdUserTeam);
            if (result.Succeeded)
            {
                var user = _userManager.FindByNameAsync(userForRegisterDTO.UserName).Result;
                await _userManager.AddToRolesAsync(user, new[] { userForRegisterDTO.roles });
                if (userForRegisterDTO.roles == "Leader")
                {
                    var TeamResult = await _context.Team.FirstOrDefaultAsync(x => x.NameOfTeam == userForRegisterDTO.team);
                    TeamResult.LeaderId = user.Id;
                    await _context.SaveChangesAsync();
                }
            }



            return result;
        }
    }
}