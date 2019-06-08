using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace PortalApp.API.Models
{
    public class UserRole : IdentityUserRole<int>
    {
        public UserModel User { get; set; }
        public Role Role { get; set; }

        
    }
}