using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;
using SenatWebAp.Infrastructure;

namespace SenatWebAp.Models
{
    public class ModelFactory
    {
        private UrlHelper _urlHelper;
        private SenatUserManager _senatUserManager;

        public ModelFactory(HttpRequestMessage request, SenatUserManager senatUserManager)
        {
            _urlHelper = new UrlHelper(request);
            _senatUserManager = senatUserManager;
        }

        public UserReturnModel Create(SenatUser senatUser)
        {
            return new UserReturnModel
            {
                Url = _urlHelper.Link("GetUserById", new {id = senatUser.Id}),
                Id = senatUser.Id,
                UserName = senatUser.UserName,
                FullName = string.Format("{0} {1}", senatUser.FirstName, senatUser.LastName),
                Email = senatUser.Email,
                EmailConfirmed = senatUser.EmailConfirmed,
                Level = senatUser.Level,
                JoinDate = senatUser.JoinDate,
                Roles = _senatUserManager.GetRolesAsync(senatUser.Id).Result,
                Claims = _senatUserManager.GetClaimsAsync(senatUser.Id).Result
            };
        }
        // вынести отдельно
        public class UserReturnModel
        {
            public string Url { get; set; }
            public string Id { get; set; }
            public string UserName { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public bool EmailConfirmed { get; set; }
            public int Level { get; set; }
            public DateTime JoinDate { get; set; }
            public IList<string> Roles { get; set; }
            public IList<System.Security.Claims.Claim> Claims { get; set; }
        }
    }
}