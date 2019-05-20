using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PortalApp.API.Models;

namespace PortalApp.API.Data
{
    public class DataContext : IdentityDbContext<UserModel, Role, int, 
    IdentityUserClaim<int>, UserRole, 
    IdentityUserLogin<int>, 
    IdentityRoleClaim<int>, IdentityUserToken<int> >
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) {}

               
        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            builder.Entity<UserTeam>()
            .HasKey(k => new {k.TeamId, k.UserId});

            builder.Entity<UserTeam>()
            .HasOne(u => u.User)
            .WithMany(u=> u.UserTeams)
            .HasForeignKey(u => u.UserId)
            .IsRequired();

            
            builder.Entity<UserTeam>()
            .HasOne(u => u.Team)
            .WithMany(u=> u.Teams)
            .HasForeignKey(u => u.TeamId)
            .IsRequired();
            
            builder.Entity<UserRole>(userRole =>{
                userRole.HasKey(ur => new {ur.UserId, ur.RoleId});
                userRole.HasOne( ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

                userRole.HasOne( ur => ur.User)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            });
        }
        public DbSet<ValueModel> Values { get; set; }

        public DbSet<Team> Team {get; set;}
        public DbSet<UserTeam> UserTeam {get;set;}
    }
}