using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PortalApp.API.Models
{
    public class Opinion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string mainText { get; set; }
        
        public string leaderText { get; set; }  

        [Required]
        public string Created { get; set; }

        
    }
}