using System.ComponentModel.DataAnnotations;

namespace PortalApp.API.Models
{
    public class UserOpinion
    {
        [Key]
        public int Id { get; set; }
        
        public UserModel User {get; set;}
        public UserModel Leader {get;set;}
        public Opinion Opinions {get; set;}
    }
}