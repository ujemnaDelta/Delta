using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using PortalApp.API.Models;

namespace PortalApp.API.Data
{
    public class Seed
    {

        private readonly UserManager<UserModel> _userManager;

        public Seed(UserManager<UserModel> userManager)
        {
            _userManager = userManager;

        }
        public void SeedUsers()
        {
            if(!_userManager.Users.Any())
            {
                var userData = System.IO.File.ReadAllText("Data/SeedData.json");
                var users = JsonConvert.DeserializeObject<List<UserModel>>(userData);
                foreach (var user in users)
                {
                    _userManager.CreateAsync(user, "password").Wait();
                }
            }
        }
    }
}