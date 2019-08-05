using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PortalApp.API.Data;
using PortalApp.API.DataTransferObjects;
using PortalApp.API.Models;
using PortalApp.API.Repositories.Interfaces;

namespace PortalApp.API.Repositories
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

        public async Task<bool> AddOpinion(Opinion opinion, OpinionDto opinionDto)
        {
            
            await _context.AddAsync(opinion);
            await _context.SaveChangesAsync();

            UserOpinion UserOpinion = new UserOpinion()
            {
                Leader = await _context.Users.SingleOrDefaultAsync(x => x.Id == opinionDto.LeaderId),
                User = await _context.Users.SingleOrDefaultAsync(x => x.Id == opinionDto.evaluatedId),
                Opinions = await _context.Opinion.SingleOrDefaultAsync(x => x.Id == opinion.Id)

            };
            await _context.UserOpinion.AddAsync(UserOpinion);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<UserModel> LeaderNameAsync(int id)
        {
            var result = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<List<Opinion>> OpinionsAsync(int id)
        {
           var result = await _context.UserOpinion.Include(x => x.Opinions).Where(x => x.User.Id == id).Select(x => x.Opinions).ToListAsync();

           return result;
        }
    }
}