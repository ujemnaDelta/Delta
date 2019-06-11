using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PortalApp.API.DataTransferObjects;
using PortalApp.API.Models;

namespace PortalApp.API.Data
{
    public interface ILeaderRepository
    {
        Task<bool> AddOpinion(Opinion opinion, OpinionDto opinionDto);

        Task<List<Opinion>> OpinionsAsync(int id);

        Task<UserModel> LeaderNameAsync(int id);
        
        

    }
}
