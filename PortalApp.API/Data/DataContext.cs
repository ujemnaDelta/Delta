using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PortalApp.API.Models;

namespace PortalApp.API.Data
{
    public class DataContext : IdentityDbContext<UserModel, Role, int,
    IdentityUserClaim<int>, UserRole,
    IdentityUserLogin<int>,
    IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });
                userRole.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

                userRole.HasOne(ur => ur.User)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
            });

        builder.Entity<UserTeam>(userTeam =>
            {
                userTeam.HasKey(ur => new { ur.UserId, ur.TeamId});
                userTeam.HasOne(ur => ur.Team)
                .WithMany(r => r.UsersTeam)
                .HasForeignKey(ur => ur.TeamId)
                .IsRequired();

                userTeam.HasOne(ur => ur.User)
                .WithMany(r => r.UserTeams)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
            });


            
            // builder.Entity<UserModel>()
            //    .HasOne(a => a.UserTeams)
            //    .WithOne()
            //    .HasForeignKey<UserTeam>(b => b.UserId);

            // builder.Entity<Team>()
            //     .HasOne(a => a.Teams)
            //     .WithOne()
            //     .HasForeignKey<UserTeam>(b => b.TeamId);
        }
        public DbSet<ValueModel> Values { get; set; }

        public DbSet<Team> Team { get; set; }
        public DbSet<UserTeam> UserTeam { get; set; }
        public DbSet<Opinion> Opinion{ get; set; }

        public DbSet<UserOpinion> UserOpinion{ get; set; }

    }
}