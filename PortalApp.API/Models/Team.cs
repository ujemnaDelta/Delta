using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalApp.API.Models
{
    public class Team
    {
        public int Id { get; set; }
        
        [Required]
        public string NameOfTeam {get; set;}


        public ICollection<UserTeam> UsersTeam {get; set;}  

        [Required]
        public int LeaderId {get; set;}
        
      


        
    }
}