using System;
using System.Threading.Tasks;
using PortalApp.API.Models;

namespace PortalApp.API.Data
{
    public class AuthorizationRepository : IAuthorizationRepository
    {
        private readonly DataContext contextGlobalField;
        public AuthorizationRepository(DataContext context)
        {
            contextGlobalField = context;
        }

        //Automatycznie wygenerowane 
        public Task<bool> IfUserExists(string UserName)
        {
            throw new System.NotImplementedException();
        }

        public Task<UserModel> LoginUser(string UserName, string UserPassword)
        {
            throw new System.NotImplementedException();
        }

        public async Task<UserModel> RegisterUser(UserModel user, string userPassword)
        {
            byte[] passwordHash;
            byte[] passwordSalt;

            CreatePassword(userPassword, out passwordHash, out passwordSalt);

            user.UserPasswordHash = passwordHash;
            user.UserPasswordSalt = passwordSalt;

            await contextGlobalField.Users.AddAsync(user);
            await contextGlobalField.SaveChangesAsync();

            return user;
        }

        private void CreatePassword(string userPassword, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hashMsg = new System.Security.Cryptography.HMACSHA512()){
                passwordHash=hashMsg.Key;
                passwordSalt=hashMsg.ComputeHash(System.Text.Encoding.UTF8.GetBytes(userPassword));
            }
        }
    }
}