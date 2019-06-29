using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SenatWebAp.Infrastructure;
using SenatWebAp.Models;

namespace SenatWebAp.Controllers
{
    
    public class LoginController : ApiController
    {
        [HttpPost]
        public async Task<IHttpActionResult> Login([FromBody] LoginModel details)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            SenatUser user = await UserManager.FindAsync(details.Name, details.Password);
            if (user == null)
                ModelState.AddModelError("", "Uncorrect Name or Password");
            else
            {
                ClaimsIdentity ident =
                    await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                AuthManager.SignOut();
                AuthManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = false
                }, ident);
            }

            return Ok();


        }
        [HttpPost]
        [Route("logout")]
        public IHttpActionResult Logout()
        {
            HttpContext.Current.GetOwinContext().Authentication.SignOut();
            return Ok();
        }

        private IAuthenticationManager AuthManager
        {
            get { return HttpContext.Current.GetOwinContext().Authentication; }
        }

        private SenatUserManager UserManager
        {
            get { return HttpContext.Current.GetOwinContext().GetUserManager<SenatUserManager>(); }
        }
    }
}
