using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PortalApp.API.Models;

namespace PortalApp.API.Data
{
    public interface ILeaderRepository
    {
        Task<IActionResult> AddOpinion(Opinion opinion);
        Task<IActionResult> AddUserOpinion(UserOpinion Useropinion);


    }
}
