using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;
using Microsoft.AspNet.Identity.EntityFramework;
using SenatWebAp.Infrastructure;

namespace SenatWebAp.Models
{
    public class Modelfactory
    {
        private UrlHelper _urlHelper;
        private SenatUserManager _senatUserManager;

        public Modelfactory(HttpRequestMessage request, SenatUserManager senatUserManager)
        {
            _urlHelper = new UrlHelper(request);
            _senatUserManager = senatUserManager;
        }

        public UserReturnModel_ Create(SenatUser senatUser)
        {
            return new UserReturnModel_
            {
                Url = _urlHelper.Link("GetUserById", new {id = senatUser.Id}),
                Id = senatUser.Id,
                UserName = senatUser.UserName,
                FullName = string.Format("{0} {1}", senatUser.FirstName, senatUser.LastName),
                Email = senatUser.Email,
                Roles = _senatUserManager.GetRolesAsync(senatUser.Id).Result
            };
        }

        public RoleReturnModel Create(IdentityRole senatRole)
        {
            return new RoleReturnModel
            {
                Url = _urlHelper.Link("GetRoleById", new {id = senatRole.Id}),
                Id = senatRole.Id,
                Name = senatRole.Name
            };
        }
    }
}