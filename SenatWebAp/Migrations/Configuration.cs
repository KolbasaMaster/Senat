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
           var manager = new UserManager<SenatUser>(new UserStore<SenatUser>(new SenatDbContext()));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new SenatDbContext()));
            var user = new SenatUser()
            {
                UserName = "Vovan",
                Email = "reutovvova@gmail.com",
                EmailConfirmed = true,
                FirstName = "Vladimir",
                LastName = "Reutov"
            
            };
            manager.Create(user, "123456");
            if (roleManager.Roles.Count() == 0)
            {
                roleManager.Create(new IdentityRole {Name = "Admin"});
                roleManager.Create(new IdentityRole { Name = "Initiator" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            var adminUser = manager.FindByName("Vovan");
            manager.AddToRoles(adminUser.Id, new string[]{"Admin"});

        }
    }
}
