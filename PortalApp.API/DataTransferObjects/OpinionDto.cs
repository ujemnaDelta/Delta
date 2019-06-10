using System;
using System.ComponentModel.DataAnnotations;

namespace PortalApp.API.DataTransferObjects
{
    public class OpinionDto
    {
        public int LeaderId { get; set; }
        public int evaluatedId{ get; set; }
        public string mainText { get; set; }
        public string leaderText { get; set; }  
        public string created { get; set; }
        public string name { get; set; }
    }
}