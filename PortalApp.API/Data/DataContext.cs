using Microsoft.EntityFrameworkCore;

namespace PortalApp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base () {

        }            
    }
}