using System.Collections.Generic;

namespace PortalApp.API.Models
{
    public class Team
    {
        public int Id { get; set; }

        public string NameOfTeam {get; set;}

        public ICollection<UserTeam> Teams {get; set;}
    }
}