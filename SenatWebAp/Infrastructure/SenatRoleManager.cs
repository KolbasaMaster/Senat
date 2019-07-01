using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace SenatWebAp.Infrastructure
{
    public class SenatRoleManager : RoleManager<IdentityRole>
    {
        public SenatRoleManager(IRoleStore<IdentityRole, string> roleStore)
            : base(roleStore)
        { }

        public static SenatRoleManager Create(IdentityFactoryOptions<SenatRoleManager> options, IOwinContext context)
        {
            var senatRolemanager = new SenatRoleManager( new RoleStore<IdentityRole>(context.Get<SenatDbContext>()));
            return senatRolemanager;
        }

    }
}