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
        private readonly RoleManager<Role> _roleManager;

        private readonly DataContext _context;
        private UserTeam newUserTeam;

        public Seed(UserManager<UserModel> userManager, RoleManager<Role> roleManager, DataContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;

        }
        public void SeedTeam() {
            _context.Entry<UserTeam>(newUserTeam).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
         
        }
        public void SeedUsers()
        {
            if(!_userManager.Users.Any())
            {
                var userData = System.IO.File.ReadAllText("Data/SeedData.json");
                var users = JsonConvert.DeserializeObject<List<UserModel>>(userData);

                var roles = new List<Role>
                {
                    new Role {Name = "Member"},
                    new Role {Name = "HR"},
                    new Role {Name = "Admin"},
                    new Role {Name = "Leader"}
                };
                   var teams = new List<Team>
                {
                    new Team {NameOfTeam = "Alpha"},
                    new Team {NameOfTeam  = "Beta"},
                    new Team {NameOfTeam  = "Gamma"},
                };
                
                foreach(var role in roles){
                    _roleManager.CreateAsync(role).Wait();
                }
                foreach(var team in teams) {
                    _context.Team.AddAsync(team);
                }
                foreach (var user in users)
                {
                    _userManager.CreateAsync(user, "password").Wait();
                    _userManager.AddToRoleAsync(user,"Member").Wait();
                    var UserTeam = new UserTeam();
                    UserTeam.User = user;
                    UserTeam.Team = teams.FirstOrDefault(p => p.Id == 1);
                    _context.UserTeam.Add(UserTeam);
                }
                var adminUser = new UserModel
                {
                    UserName = "Admin"
                };
                var HRUser = new UserModel
                {
                    UserName = "HR"
                };
                var LeaderUser = new UserModel
                {
                    UserName = "Leader"
                };
                IdentityResult result = _userManager.CreateAsync(adminUser, "password").Result;
                IdentityResult result2 = _userManager.CreateAsync(HRUser, "password").Result;
                IdentityResult result3 = _userManager.CreateAsync(LeaderUser, "password").Result;
                if(result.Succeeded && result2.Succeeded && result3.Succeeded)
                {
                    var admin = _userManager.FindByNameAsync("Admin").Result;
                    _userManager.AddToRolesAsync(admin, new [] {"Admin"}).Wait();

                    var hr = _userManager.FindByNameAsync("HR").Result;
                    _userManager.AddToRolesAsync(hr, new [] {"HR"}).Wait();

                    var leader = _userManager.FindByNameAsync("Leader").Result;
                    _userManager.AddToRolesAsync(leader, new [] {"Leader"}).Wait();
                }
            }
        }
    }
}