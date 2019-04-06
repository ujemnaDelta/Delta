using System.ComponentModel.DataAnnotations;

namespace PortalApp.API.DataTransferObjects
{
    public class UserForRegisterDTO
    {
        [Required]
        public string UserName { get; set; }       
        [Required]
        [StringLength(32, MinimumLength = 8, ErrorMessage="Password bettween 8 and 32")]
        public string UserPassword { get; set; }
    }
}