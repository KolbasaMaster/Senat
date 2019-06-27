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
    public class SenatUserManager : UserManager<SenatUser>
    {
        public SenatUserManager(IUserStore<SenatUser> store)
            :base(store)
        { }

        public static SenatUserManager Create(IdentityFactoryOptions<SenatUserManager> options, IOwinContext context)
        {
            var appDbContext = context.Get<SenatDbContext>();
            var appUserManager = new SenatUserManager(new UserStore<SenatUser>(appDbContext));
            appUserManager.UserValidator = new UserValidator<SenatUser>(appUserManager)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true
            };
            return appUserManager;
        }
    }
}