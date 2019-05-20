namespace PortalApp.API.Models
{
    public class UserTeam
    {
        public int UserId {get; set;}

        public int TeamId {get; set;}
        public UserModel User{get; set;}
        public Team Team {get;set;}


    }
}