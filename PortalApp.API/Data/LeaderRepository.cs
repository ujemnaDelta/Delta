using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PortalApp.API.Models;

namespace PortalApp.API.Data
{
    public class LeaderRepository : ILeaderRepository
    {
        private readonly DataContext _context;
         private readonly UserManager<UserModel> _userManager;
        public LeaderRepository(DataContext context, UserManager<UserModel> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> AddOpinion(Opinion opinion)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IActionResult> AddUserOpinion(UserOpinion Useropinion)
        {
            throw new System.NotImplementedException();
        }

    }
}