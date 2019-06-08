using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalApp.API.Models
{
    public class Team
    {
        public int Id { get; set; }

        public string NameOfTeam {get; set;}

        public UserRole User {get; set;}

        public ICollection<UserTeam> UsersTeam {get; set;}  

        public int LeaderId {get; set;}
        
      


        
    }
}