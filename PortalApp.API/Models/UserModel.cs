using System;

namespace PortalApp.API.Models
{
    public class UserModel
    {
        public int Id { get; set; }   
        public string UserName { get; set; }
        public byte[] UserPasswordHash { get; set; }
        public byte[] UserPasswordSalt { get; set; }
    }
}