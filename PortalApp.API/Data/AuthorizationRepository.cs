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
        public async Task<bool> IfUserExists(string userName)
        {
            if(await contextGlobalField.Users.AnyAsync(x=> x.UserName == userName)){
                return true;
            }
            return false;
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

            return user;
        }

        private bool CheckPassword(string userPassword, byte[] userPasswordHash, byte[] userPasswordSalt)
        {
            //do skasowania potem moi drodzy bo jak będzie nullem to wywali aplikację :*
            if(userPassword == null){
                throw new System.ArgumentException("PASSWORD JEST NULLEM", "Tymon");
            }
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
                passwordSalt=hashMsg.Key;
                passwordHash=hashMsg.ComputeHash(System.Text.Encoding.UTF8.GetBytes(userPassword));
            }
        }
    }
}