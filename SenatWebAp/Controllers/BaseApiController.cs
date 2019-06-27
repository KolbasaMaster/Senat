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

namespace SenatWebAp.Controller
{
    public class BaseApiController : ApiController
    {
        private ModelFactory _modelFactory;
        private SenatUserManager _senatUserManager = null;

        protected SenatUserManager senatUserManager
        {
            get { return _senatUserManager ?? Request.GetOwinContext().GetUserManager<SenatUserManager>(); }
        }
        public BaseApiController()
        { }

        protected ModelFactory TheModelFactory
        {
            get
            {
                if(_modelFactory == null)
                    _modelFactory = new ModelFactory(this.Request, this.senatUserManager);
                return _modelFactory;
            }
        }

        protected IHttpActionResult GetErroResult(IdentityResult result)
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
