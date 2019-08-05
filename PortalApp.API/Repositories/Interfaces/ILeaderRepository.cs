using System.Collections.Generic;
using System.Threading.Tasks;
using PortalApp.API.DataTransferObjects;
using PortalApp.API.Models;

namespace PortalApp.API.Repositories.Interfaces
{
    public interface ILeaderRepository
    {
        Task<bool> AddOpinion(Opinion opinion, OpinionDto opinionDto);

        Task<List<Opinion>> OpinionsAsync(int id);

        Task<UserModel> LeaderNameAsync(int id);
        
        

    }
}
