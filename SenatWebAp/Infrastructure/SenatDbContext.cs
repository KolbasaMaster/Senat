using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SenatWebAp.Infrastructure
{
    public class SenatDbContext : IdentityDbContext<SenatUser>
    {
        public SenatDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
           Configuration.LazyLoadingEnabled = false;
        }

        public static SenatDbContext Create()
        {
            return new SenatDbContext();
        }
    }
}