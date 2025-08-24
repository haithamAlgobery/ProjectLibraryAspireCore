using Domain.Entities;
using Infrastructure.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = RolesName.AdminId,
                    Name = RolesName.Admin,
                    NormalizedName = RolesName.Admin.ToUpper()
                },
                new IdentityRole
                {
                    Id = RolesName.ModeratorId,
                    Name = RolesName.Moderator,
                    NormalizedName = RolesName.Moderator.ToUpper()
                },
                new IdentityRole
                {
                    Id = RolesName.UserId,
                    Name = RolesName.User,
                    NormalizedName = RolesName.User.ToUpper()
                }
            );

        }
    }
}
