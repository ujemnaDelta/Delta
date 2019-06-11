using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalApp.API.Data;
using PortalApp.API.Models;

namespace PortalApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly UserManager<UserModel> _userManager;

        public UserController(DataContext context, UserManager<UserModel> userManager)
        {
            _context = context;
            _userManager = userManager;
           
        }
        
        [AllowAnonymous]
        [HttpGet("useropinions/{id}")]
        public async Task<IActionResult> GetUsersOpinionAsync(int id)
        {
            var result = await _context.UserOpinion.Include(x => x.Opinions).Where(x => x.User.Id == id).Select(x => new
                               {
                                    created = x.Opinions.Created,
                                    mainText = x.Opinions.mainText,
                                    leaderName = x.Leader.FullUserName,
                                    name = x.User.FullUserName,
                                    evaluatedId = id,
                                    position = x.User.Position

                               }).ToListAsync();
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("userteam/{id}")]
        public async Task<IActionResult> GetTeam(int id)
        {
            if (id == 0)
            {
                BadRequest("Błędne id");
            }

            var team2 = await _context.UserTeam.FirstOrDefaultAsync(x=>x.UserId == id);
            
            var TeamMates = _context.UserTeam.Include(p => p.User).Where(p => p.TeamId == team2.TeamId).Select(p => new
            {
                Name = p.User.FullUserName,
                Position = p.User.Position,
                Id = p.User.Id
            }).ToList();


            return Ok(TeamMates);
        }
    }
}