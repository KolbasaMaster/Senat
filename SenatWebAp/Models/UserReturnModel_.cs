﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SenatWebAp.Models
{
    public class UserReturnModel_
    {
        public string Url { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public IList<string> Roles { get; set; }
        public IList<System.Security.Claims.Claim> Claims { get; set; }
    }
}