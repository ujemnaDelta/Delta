using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace PortalApp.API.Models
{
    public class UserModel : IdentityUser<int>
    {
        public string FullUserName { get; set; }

        public ICollection<UserTeam> UserTeams {get; set;}
        public ICollection<UserRole> UserRoles { get; set; }

        public ICollection<Team> Teams{get; set;}
    }
}