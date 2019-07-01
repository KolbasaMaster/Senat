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
using System.Web.Http.Description;
using System.Web.UI.WebControls;
using RestSharp;
using SenatWebAp.Models;

namespace SenatWebAp.Controller
{
    [Authorize]
    [RoutePrefix("api/users")]
    
    public class UsersController : BaseUserApiController
    {
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IHttpActionResult GetUsers()
        {
            
            return Ok(this.senatUserManager.Users.ToList().Select(u => this.Modelfactory.Create(u)));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("user/{id:guid}", Name = "GetUserById")]
        public async Task<IHttpActionResult> GetUser(string Id)
        {
            var user = await this.senatUserManager.FindByIdAsync(Id);
            if (user != null)
            {
                return Ok(this.Modelfactory.Create(user));
            }

            return NotFound();
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ResponseType(typeof(UserReturnModel_))]
        public async Task<IHttpActionResult> CreateUser(CreateUserBindingModel createUserModel)
        {
            var user = new SenatUser()
            {
                UserName = createUserModel.Username,
                Email = createUserModel.Email,
                FirstName = createUserModel.FirstName,
                LastName = createUserModel.LastName
            };
            IdentityResult addUserResult = await this.senatUserManager.CreateAsync(user, createUserModel.Password);
            if (!addUserResult.Succeeded)
            {
                return GetErrorResult(addUserResult);
            }

            await senatUserManager.AddToRolesAsync(user.Id, createUserModel.RoleName);

            Uri locationHeader = new Uri(Url.Link("GetUserById", new { id = user.Id }));
            return Created(locationHeader, Modelfactory.Create(user));
        }


        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            IdentityResult result = await this.senatUserManager.ChangePasswordAsync(User.Identity.GetUserId(),
                model.OldPassword, model.NewPassword);
            if (!result.Succeeded)
                return GetErrorResult(result);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        [Route("user/{id:guid}/roles")]
        [HttpPut]
        public async Task<IHttpActionResult> AssignRolesToUser([FromUri] string id, [FromBody] string[] rolesToAssign)
        {

            var appUser = await this.senatUserManager.FindByIdAsync(id);

            if (appUser == null)
            {
                return NotFound();
            }

            var currentRoles = await this.senatUserManager.GetRolesAsync(appUser.Id);

            var rolesNotExists = rolesToAssign.Except(this.SenatRoleManager.Roles.Select(x => x.Name)).ToArray();

            if (rolesNotExists.Count() > 0)
            {

                ModelState.AddModelError("", string.Format("Roles '{0}' does not exixts in the system", string.Join(",", rolesNotExists)));
                return BadRequest(ModelState);
            }

            IdentityResult removeResult = await this.senatUserManager.RemoveFromRolesAsync(appUser.Id, currentRoles.ToArray());

            if (!removeResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to remove user roles");
                return BadRequest(ModelState);
            }

            IdentityResult addResult = await this.senatUserManager.AddToRolesAsync(appUser.Id, rolesToAssign);

            if (!addResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to add user roles");
                return BadRequest(ModelState);
            }

            return Ok();
        }
    }
}
