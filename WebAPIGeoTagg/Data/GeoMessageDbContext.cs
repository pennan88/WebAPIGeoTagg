﻿using Microsoft.AspNetCore.Identity;
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
    public class GeoMessageDbContext : IdentityDbContext<IdentityUser>
    {

        public GeoMessageDbContext(DbContextOptions<GeoMessageDbContext> options) :
            base(options)
        {

        }

        public DbSet<GeoMessage> GeoMessages { get; set; }
        public DbSet<User> Users { get; set; }
        public static async Task Reset(IServiceProvider provider)
        {
            var context = provider.GetRequiredService<GeoMessageDbContext>();
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
