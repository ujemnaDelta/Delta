using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PortalApp.API.Data
{
    public class AdminRepository : IAdminPanelRepository
    {
        private readonly DataContext _context;

        public AdminRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<string>> AllRoles()
        {
            var roles = await _context.UserRoles.Include(p => p.Role)
                                        .Select(p => p.Role.Name).Distinct().ToListAsync();
            return roles;
        }

        public async Task<List<string>> AllTeams()
        {
            var team = await _context.Team.Select(p => p.NameOfTeam).ToListAsync();
            return team;
        }
    }
}