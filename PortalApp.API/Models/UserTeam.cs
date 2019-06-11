using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalApp.API.Models
{
    public class UserTeam
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int TeamId { get; set; }
        public UserModel User {get; set;}
        public Team Team {get;set;}

    }
}