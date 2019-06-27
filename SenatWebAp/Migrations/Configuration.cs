using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SenatWebAp.Infrastructure;

namespace SenatWebAp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SenatWebAp.Infrastructure.SenatDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SenatWebAp.Infrastructure.SenatDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            var manager = new UserManager<SenatUser>(new UserStore<SenatUser>(new SenatDbContext()));
            var user = new SenatUser()
            {
                UserName = "SuperPowerUser",
                Email = "reutovvova@gmail.com",
                EmailConfirmed = true,
                FirstName = "Vladimir",
                LastName = "Reutov",
                Level = 1,
                JoinDate = DateTime.Now.AddYears(-3)
            };
            manager.Create(user, "MySuperP@ssword");
          
        }
    }
}
