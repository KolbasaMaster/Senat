using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Validation.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SenatWebAp.Infrastructure;
using SenatWebAp.Models;
//паттерн стратегия, композиция
namespace SenatWebAp.Controller
{
    public class BaseUserApiController : ApiController
    {
        private Modelfactory _modelfactory;
        private SenatUserManager _senatUserManager = null;//singleton 
        private SenatRoleManager _senatRoleManager = null;

        protected SenatUserManager senatUserManager
        {
            get { return _senatUserManager ?? Request.GetOwinContext().GetUserManager<SenatUserManager>(); }
        }

        protected SenatRoleManager SenatRoleManager
        {
            get { return _senatRoleManager ?? Request.GetOwinContext().GetUserManager<SenatRoleManager>(); }
        }
        public BaseUserApiController()
        { }

        protected Modelfactory Modelfactory
        {
            get
            {
                if(_modelfactory == null)
                    _modelfactory = new Modelfactory(this.Request, this.senatUserManager);
                return _modelfactory;
            }
        }

        protected IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
                return InternalServerError();
            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
