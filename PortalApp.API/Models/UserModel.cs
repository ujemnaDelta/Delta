using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace PortalApp.API.Models
{
    public class UserModel : IdentityUser<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}