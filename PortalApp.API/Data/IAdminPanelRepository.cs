using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalApp.API.Data
{
    public interface IAdminPanelRepository
    {
         Task<List<string>> AllRoles();
         Task<List<string>> AllTeams();

    }
}