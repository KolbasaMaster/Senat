using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Web.UI.WebControls;
using RestSharp;
using SenatWebAp.Models;

namespace SenatWebAp.Controller
{
    [Authorize]
    [RoutePrefix("api/users")]
    public class UsersController : BaseUserApiController
    {
        
        [HttpGet]
        public IHttpActionResult GetUsers()
        {
            
            return Ok(this.senatUserManager.Users.ToList().Select(u => this.ModelFactory.Create(u)));
        }

        [HttpGet]
        [Route("user/{id:guid}", Name = "GetUserById")]
        public async Task<IHttpActionResult> GetUser(string Id)
        {
            var user = await this.senatUserManager.FindByIdAsync(Id);
            if (user != null)
            {
                return Ok(this.ModelFactory.Create(user));
            }

            return NotFound();
        }

        
        [HttpPost]
        public async Task<IHttpActionResult> CreateUser(CreateUserBindingModel createUserModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new SenatUser()
            {
                UserName = createUserModel.Username,
                Email = createUserModel.Email,
                FirstName = createUserModel.FirstName,
                LastName = createUserModel.LastName,
                Level = 3,
                JoinDate = DateTime.Now.Date
            };
            IdentityResult addUserResult = await this.senatUserManager.CreateAsync(user, createUserModel.Password);
            if (!addUserResult.Succeeded)
            {
                return GetErrorResult(addUserResult);
            }

            Uri locationHeader = new Uri(Url.Link("GetUserById", new { id = user.Id }));
            return Created(locationHeader, ModelFactory.Create(user));
        }

        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            IdentityResult result = await this.senatUserManager.ChangePasswordAsync(User.Identity.GetUserId(),
                model.OldPassword, model.NewPassword);
            if (!result.Succeeded)
                return GetErrorResult(result);
            return Ok();
        }

        [Route("user/{id:guid}")]
        public async Task<IHttpActionResult> DeleteUser(string id)
        {
            var appUser = await this.senatUserManager.FindByIdAsync(id);
            if (appUser != null)
            {
                IdentityResult result = await this.senatUserManager.DeleteAsync(appUser);

                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }
                return Ok();
            }
            return NotFound();
        }

    }
}
