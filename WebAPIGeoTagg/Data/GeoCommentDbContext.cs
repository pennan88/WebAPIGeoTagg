using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIGeoTagg.Models;

namespace WebAPIGeoTagg.Data
{
    public class GeoCommentDbContext : IdentityDbContext<IdentityUser>
    {

        public GeoCommentDbContext(DbContextOptions<GeoCommentDbContext> options) :
            base(options)
        {

        }
        public DbSet<GeoCommentVersion2> GeoComment2 { get; set; }
        public DbSet<Message> messages { get; set; }

        //public DbSet<User> Users { get; set; }
        public static async Task Reset(IServiceProvider provider)
        {
            var context = provider.GetRequiredService<GeoCommentDbContext>();
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            var userManager = provider.GetRequiredService<UserManager<IdentityUser>>();
            await userManager.CreateAsync(
            new IdentityUser
            {
                UserName = "TestUser"
            },
            "QWEqwe123!");

        }

    }
}
