using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
        public Task<bool> IfUserExists(string userName)
        {
            throw new System.NotImplementedException();
        }

        public async Task<UserModel> LoginUser(string userName, string userPassword)
        {
            var user = await contextGlobalField.Users.FirstOrDefaultAsync(x => x.UserName == userName); 

            if(user == null){
                return null;
            }

            if(!CheckPassword(userPassword, user.UserPasswordHash, user.UserPasswordSalt)){
                return null;
            }
        }

        private bool CheckPassword(string userPassword, byte[] userPasswordHash, byte[] userPasswordSalt)
        {
            using (var hashMsg = new System.Security.Cryptography.HMACSHA512(userPasswordSalt)){
                
                var calculatedHash = hashMsg.ComputeHash(System.Text.Encoding.UTF8.GetBytes(userPassword));

                for(int i = 0; i < calculatedHash.Length; i++){
                    if(calculatedHash[i] != userPasswordHash[i]) 
                        return false;
                }
            }return true;
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