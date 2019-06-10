using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalApp.API.Data;
using PortalApp.API.DataTransferObjects;
using PortalApp.API.Models;

namespace PortalApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaderController : ControllerBase
    {
        private readonly ILeaderRepository _leaderRepo;
        private readonly DataContext _context;
        private readonly UserManager<UserModel> _userManager;

        public LeaderController(DataContext context, UserManager<UserModel> userManager, ILeaderRepository leaderRepo)
        {
            _context = context;
            _userManager = userManager;
            _leaderRepo = leaderRepo;
        }

        [Authorize(Policy = "RequireLeader")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLeaderName(int id)
        {
            if(id == 0) {
                return BadRequest("Złe id");
            }
            var leader= await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            return Ok(leader);
        }

        [Authorize(Policy = "RequireLeader")]
        [HttpGet("leaderteam/{id}")]
        public async Task<IActionResult> GetTeam(int id)
        {
            if (id == 0)
            {
                BadRequest("Błędne id");
            }
            var team2 = await _context.Team.FirstOrDefaultAsync(x => x.LeaderId == id);

            var TeamMates = _context.UserTeam.Include(p => p.User).Where(p => p.TeamId == team2.Id).Select(p => new
            {
                Name = p.User.FullUserName,
                Position = p.User.Position,
                Id = p.User.Id
            }).ToList();


            var leader = TeamMates.Single(x => x.Id == id);
            TeamMates.Remove(leader);
            return Ok(TeamMates);
        }

        [Authorize(Policy = "RequireLeader")]
        [HttpPost("opinion")]
        public async Task<IActionResult> AddOpinion(OpinionDto opinionDto)
        {
            if (opinionDto.mainText == null || opinionDto.mainText.Trim() == "" || opinionDto.mainText.Trim() == null)
            {
                return BadRequest("Nie można dodać pustej opinii");
            }
            Opinion opinion = new Opinion()
            {
                leaderText = opinionDto.leaderText,
                mainText = opinionDto.mainText,
                Created = opinionDto.created
            };
            var opinionAdded = await _context.AddAsync(opinion);
            await _context.SaveChangesAsync();

            UserOpinion UserOpinion = new UserOpinion()
            {
                Leader = await _context.Users.SingleOrDefaultAsync(x => x.Id == opinionDto.LeaderId),
                User = await _context.Users.SingleOrDefaultAsync(x => x.Id == opinionDto.evaluatedId),
                Opinions = await _context.Opinion.SingleOrDefaultAsync(x => x.Id == opinion.Id)

            };
            await _context.UserOpinion.AddAsync(UserOpinion);
            await _context.SaveChangesAsync();

            return Ok(201);
        }
        [Authorize(Policy = "RequireLeader")]
        [HttpGet("useropinions/{id}")]
        public async Task<IActionResult> GetOpinions(int id)
        {

            var result = await _context.UserOpinion.Include(x => x.Opinions).Where(x => x.User.Id == id).Select(x => x.Opinions).ToListAsync();
            return Ok(result);
        }
    }
}