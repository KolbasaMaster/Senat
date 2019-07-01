using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using SenatApi;


namespace SenatWebAp
{
    
    public class IssueController : ApiController
    {
        //todo: move to autofac
        private RestSenatApiClient_ _client;

        public IssueController(RestSenatApiClient_ client)
        {
            _client = client;
        }

        [Authorize(Roles = "Initiator, User")]
        [HttpGet]
        public IHttpActionResult GetIssues()
        {
            return Ok(_client.GetListOfIssues());
        }

        [Authorize(Roles = "Initiator, User")]
        [HttpGet]
        public IssueMultilingualDto GetIssue(Guid id)
        {
            return _client.GetIssue(id);
        }

        [Authorize(Roles = "Initiator")]
        [HttpPost]
        public IHttpActionResult PostIssue([FromBody] IssueDto issue)
        {
            return Ok(_client.CreateIssue(issue));
        }
    }
}